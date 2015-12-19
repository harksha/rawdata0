using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ReadOnly
{
	public class AnswerMapper : DataMapper<Answer>
	{
		public AnswerMapper(string connectionString) : base(connectionString)
		{
			TableName = "post";
			Attributes = new string[] { "body", "score", "title", "creationDate", "ownerUserId", "parentQuestionID" };
		}

		public override Answer Map(MySqlDataReader reader)
		{
			if (reader.Read()&&reader.HasRows)
			{
				int a_id, a_score, a_parent;
				string a_body, a_title, a_owner;
				DateTime a_date;

				if (!reader.IsDBNull(0)) { a_id = reader.GetInt32(0); }
				else { a_id = 0; }
				if (!reader.IsDBNull(1)) { a_body = reader.GetString(1); }
				else { a_body = "unknown"; }
				if (!reader.IsDBNull(2)) { a_score = reader.GetInt32(2); }
				else { a_score = 0; }
				if (!reader.IsDBNull(3)) { a_title = reader.GetString(3); }
				else { a_title = "unknown"; }
				if (!reader.IsDBNull(4)) { a_date = reader.GetDateTime(4); }
				else { a_date = DateTime.MinValue; }
				if (!reader.IsDBNull(5)) { a_owner = FetchOwnername(reader.GetInt32(5)); }
				else { a_owner = "unknown"; }
				if (!reader.IsDBNull(6)) { a_parent =  reader.GetInt32(6); }
				else { a_parent = 0; }

				var Answer = new Answer
				{					
					Id = a_id,
					Body = a_body,
					Score = a_score,
					CreationDate = a_date,
					Owner = a_owner,
					ParentId = a_parent
				};
				return Answer;
			}
			return null;
		}

		private string FetchOwnername(int id)
		{
			using (var connection = new MySqlConnection(ConnectionString))
			{
				connection.Open();
				var cmd = new MySqlCommand();
				cmd.Connection = connection;
				cmd.CommandText = "select displayName from user where  id= @ID";
				cmd.Parameters.AddWithValue("@ID", id);
				using (var reader = cmd.ExecuteReader())
				{
					while (reader.Read())
					{
						string uname = reader.GetString(0);
						return uname;
					}
					return "unknown";
				}
			}
		}
	}
}
