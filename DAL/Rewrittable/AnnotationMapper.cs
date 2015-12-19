using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Rewrittable
{
	public class AnnotationMapper : UpdatableDataMapper<Annotation>
	{
		public UserMapper userMapper { get; set; }
		public AnnotationMapper(string connectionString) : base(connectionString)
		{
			TableName = "annotation";
			Attributes = new string[] { "body", "date", "postid", "userid" };
		}
		public override Annotation GetByPostAndUser(int postid, int userid)
		{
			var sql = string.Format("SELECT ID, {0} from {1} WHERE postID = @postID and userID=@userID", AttributeList, TableName);
			using (var connection = new MySqlConnection(ConnectionString))
			{
				connection.Open();
				using (var cmd = new MySqlCommand(sql))
				{
					cmd.Parameters.AddWithValue("@postID", postid);
					cmd.Parameters.AddWithValue("@userID", userid);
					cmd.Connection = connection;
					using (var reader = cmd.ExecuteReader())
					{
						if (reader.HasRows)
						{
							return Map(reader);
						}
						return null;
					}
				}
			}

		}

		public override int Insert(Annotation annotation)
		{
			var sql = string.Format("insert into {0} ({1}) values({2})",
				TableName, AttributeList, DecoratedAttributeList(x => "@" + x));
			var cmd = new MySqlCommand(sql);
			cmd.Parameters.AddWithValue("@" + Attributes[0], annotation.Body);
			cmd.Parameters.AddWithValue("@" + Attributes[1], annotation.Date);
			cmd.Parameters.AddWithValue("@" + Attributes[2], annotation.PostId);
			cmd.Parameters.AddWithValue("@" + Attributes[3], annotation.UserId);
			return ExecuteNonQuery(cmd);
		}

		/**
		*   I am sorry that to commend out your code ioana. I know you have implemented 
		*   very fancy function here , but it can not pass my test and I need this function 
		*   to work for the displaying data on webpage.
		*   panpan wrote . 
		public override void Update(Annotation annotation)
		{
			// CHANGE THIS 
			var sql = string.Format("update {0} set {1} where postid= @postid",
				TableName, AttributeList, DecoratedAttributeList((x => x + "=@" + x)));
			//  annotation.Id = NextId();
			var cmd = new MySqlCommand(sql);
			cmd.Parameters.AddWithValue("@postid", annotation.Id);
			cmd.Parameters.AddWithValue("@" + Attributes[0], annotation.Body);
			ExecuteNonQuery(cmd);

		}*/

		public override int Update(Annotation a)
		{
			var sql = "update " + TableName + " set body = '" + a.Body + "' and date ='" + DateTime.Now.ToString("s") + "'  where postID =" + a.PostId + " and userID=" + a.UserId;
			var cmd = new MySqlCommand(sql);
			return ExecuteNonQuery(cmd);
		}
		public override Annotation Map(MySqlDataReader reader)
		{
			if (reader.Read())
			{
				int a_id, a_postId, a_userId;
				string a_body;
				DateTime a_date;


				if (!reader.IsDBNull(0)) { a_id = reader.GetInt32(0); }
				else { a_id = 0; }
				if (!reader.IsDBNull(1)) { a_body = reader.GetString(1); }
				else { a_body = "unknown"; }
				if (!reader.IsDBNull(2)) { a_date = reader.GetDateTime(2); }
				else { a_date = DateTime.Now; }
				if (!reader.IsDBNull(3)) { a_postId = reader.GetInt32(3); } else { a_postId = 0; }
				if (!reader.IsDBNull(4)) { a_userId = reader.GetInt32(4); } else { a_userId = 0; }

				var annotation = new Annotation
				{
					Id = a_id,
					Body = a_body,
					Date = a_date,
					PostId = a_postId,
					UserId = a_userId

				};
				return annotation;
			}
			return null;
		}
	}
}
