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
			Attributes = new string[] { "postId","text", "creationDate","Userid" };
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
				
				string a_body;
				string a_date;
				string a_id;

				if (!reader.IsDBNull(0)) { a_body = reader.GetString(0); }
				else { a_id = "unknown"; }
				if (!reader.IsDBNull(1)) { a_body = reader.GetString(1); }
				else { a_body = "unknown"; }
				if (!reader.IsDBNull(2)) { a_date = reader.GetString(2); }
				else { a_date = "unknown"; }
				
				var comment = new Comment
				{
					
					Text = a_body,
					CreationDate = a_date


			

				};
				return comment;
			}
			return null;
		}

	}
}
