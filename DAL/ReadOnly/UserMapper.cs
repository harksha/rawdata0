using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
	public class UserMapper : DataMapper<User>
	{
		public UserMapper(string connectionString) : base(connectionString)
		{
			TableName = "user";
			Attributes = new string[] { "displayName", "websiteUrl", "reputation", "creationDate", "age", "upVotes", "downVotes", "locationId" };
		}
		public override User Map(MySqlDataReader reader)
		{
			if (reader.Read() && reader.HasRows)
			{
				int uid, u_rep, u_age, u_up, u_down;
				string u_name, u_websiteUrl, location;
				DateTime u_creationDate;
				if (!reader.IsDBNull(0)) { uid = reader.GetInt32(0); }
				else { uid = 0; }
				if (!reader.IsDBNull(1)) { u_name = reader.GetString(1); }
				else { u_name = "unknown"; }
				if (!reader.IsDBNull(2)) { u_websiteUrl = reader.GetString(2); }
				else { u_websiteUrl = "unknown"; }
				if (!reader.IsDBNull(3)) { u_rep = reader.GetInt32(3); }
				else { u_rep = 0; }
				if (!reader.IsDBNull(4)) { u_creationDate = reader.GetDateTime(4); }
				else { u_creationDate = DateTime.Now; }
				if (!reader.IsDBNull(5)) { u_age = reader.GetInt32(5); }
				else { u_age = 0; }
				if (!reader.IsDBNull(6)) { u_up = reader.GetInt32(6); }
				else { u_up = 0; }
				if (!reader.IsDBNull(7)) { u_down = reader.GetInt32(7); }
				else { u_down = 0; }
				if (!reader.IsDBNull(8)) { location = FetchLocation(reader.GetInt32(8)); }
				else { location = "unknown"; }
				var user = new User
				{
					Id = uid,
					Name = u_name,
					WebsiteUrl = u_websiteUrl,
					Reputation = u_rep,
					CreationDate = u_creationDate,
					Age = u_age,
					UpVotes = u_up,
					DownVotes = u_down,
					Location = location
				};
				return user;
			}
			return null;
		}

		private string FetchLocation(int id)
		{
			using (var connection = new MySqlConnection(ConnectionString))
			{
				connection.Open();
				var cmd = new MySqlCommand();
				cmd.Connection = connection;
				cmd.CommandText = "select location from location where locationid = @ID";
				cmd.Parameters.AddWithValue("@ID", id);
				using (var reader = cmd.ExecuteReader())
				{
					while (reader.Read())
					{
						string loc = reader.GetString(0);
						return loc;
					}
					return "not found";
				}
			}
		}
	}
}
