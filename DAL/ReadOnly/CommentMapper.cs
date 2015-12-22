using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace DAL.ReadOnly
{
	public class CommentMapper : DataMapper<Comment>
	{
		public CommentMapper(string connectionSting) : base(connectionSting)
		{

			TableName = "comment";
			Attributes = new string[] { "userId", "postId",  "text", "creationDate" };
		}
		/*public override Comment GetById(int postid)
		{
			var sql = string.Format("SELECT postID, {0} from {1} WHERE postID = @postID", AttributeList, TableName);
			using (var connection = new MySqlConnection(ConnectionString))
			{
				connection.Open();
				using (var cmd = new MySqlCommand(sql))
				{
					cmd.Parameters.AddWithValue("@postID", postid);
					cmd.Connection = connection;
					using (var reader = cmd.ExecuteReader())
					{
						return Map(reader);
					}
				}
			}
		}*/

		public override Comment Map(MySqlDataReader reader)
		{
			if (reader.Read()&& reader.HasRows)
			{
				
				int p_id, id, u;
				DateTime a_date;
				string text;


				if (!reader.IsDBNull(0)) { id = reader.GetInt32(0); }
				else { id = 0; }
				if (!reader.IsDBNull(1)) { u = reader.GetInt32(1); }
				else { u = 0; }
				if (!reader.IsDBNull(2)) { p_id = reader.GetInt32(2); }
				else { p_id = 0; }
				
				if (!reader.IsDBNull(3)) { text = reader.GetString(3); }
				else { text = "unknown"; }
				if (!reader.IsDBNull(4)) { a_date = reader.GetDateTime(4); }
				else { a_date = DateTime.Now; }
				

				var comment = new Comment
				{
					Id=id,
					PostId=p_id,
					userId = u,
                    Text = text,
					CreationDate = a_date,
					
			

				};
				return comment;
			}
			return null;
		}

	}
}
