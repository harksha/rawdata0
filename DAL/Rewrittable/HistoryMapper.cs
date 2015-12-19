using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Rewrittable
{
	public class HistoryMapper : UpdatableDataMapper<History>
	{
		public HistoryMapper(string connectionString) : base(connectionString)
		{
			TableName = "SearchHistory";
			Attributes = new string[] { "body", "searchdate", "userid" };
		}
		/*public override IEnumerable<History> GetById(int userid)
		{
			var sql = string.Format("SELECT userID, {0} from {1} WHERE userID = @userID", AttributeList, TableName);
			using (var connection = new MySqlConnection(ConnectionString))
			{
				connection.Open();
				using (var cmd = new MySqlCommand(sql))
				{
					cmd.Parameters.AddWithValue("@userID", userid);
					cmd.Connection = connection;
					using (var reader = cmd.ExecuteReader())
					{
						return UpdatableDataMapper.Query(new MySqlCommand);
					}
				}
			}
		}*/

		public override int  Insert(History history)
		{
			var sql = string.Format("insert into {0} ({1}) values({2})",
				TableName, AttributeList, DecoratedAttributeList(x => "@" + x));
			var cmd = new MySqlCommand(sql);
			cmd.Parameters.AddWithValue("@" + Attributes[0], history.Body);
			cmd.Parameters.AddWithValue("@" + Attributes[1], history.Date);
			cmd.Parameters.AddWithValue("@" + Attributes[2], history.UserId);
			return ExecuteNonQuery(cmd);
		}
		public override History GetByPostAndUser(int post, int user)
		{
			return new History { };
		}

		public override History Map(MySqlDataReader reader)
		{
			if (reader.Read() && reader.HasRows)
			{
				int a_id, a_userId;
				string a_body;
				DateTime a_date;


				if (!reader.IsDBNull(0)) { a_id = reader.GetInt32(0); }
				else { a_id = 0; }
				if (!reader.IsDBNull(1)) { a_body = reader.GetString(1); }
				else { a_body = "unknown"; }
				if (!reader.IsDBNull(2)) { a_date = reader.GetDateTime(2); }
				else { a_date = DateTime.Now; }
				if (!reader.IsDBNull(3)) { a_userId = reader.GetInt32(3); }
				else { a_userId = 0; }

				var history = new History
				{
					Id = a_id,
					Body = a_body,
					Date = a_date,
					UserId = a_userId

				};
				return history;
			}
			return null;
		}

		public override int Update(History history)
		{
			throw new NotImplementedException("this function is not supported in this project");
		}

	}
}
