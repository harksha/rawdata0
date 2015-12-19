
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace DAL.ReadOnly
{
	public class CommentRepository : Repository<Comment>
	{
		public CommentRepository(string connectionString) : base(new CommentMapper(connectionString)) { }


		/*public int Limit { get; set; }
		public int Offset { get; set; }
		public string ConnectionString { get; set; }
		public CommentRepository(IDataMapper<Comment> commentMapper) : base(commentMapper)
		{
		}
		public IEnumerable<Comment> getByPostId(int postId)
		{
			string sql = string.Format("select * from comment where postId = {0}", postId);
			return DataMapper.Query(new MySqlCommand(sql));
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="creationDate"></param>
		/// <returns></returns>
		public IEnumerable<Comment> getBycreationDate(DateTime creationDate)
		{
			string sql = string.Format("select * from comment where creationDate = {0:yyyy-mm-dd hh-mm-ss}",
				   creationDate);
			return DataMapper.Query(new MySqlCommand(sql));
		}

		public IEnumerable<Comment> getByUserId(int userId)
		{
			string sql = string.Format("select * from comment where userid = {0}", userId);
			return DataMapper.Query(new MySqlCommand(sql));
		}
		public IEnumerable<Comment> getByKeyWord(string keyword)
		{
			string sql = string.Format("select * from comment where text like '%{0}%'", keyword);
			return DataMapper.Query(new MySqlCommand(sql));
		}

		private IEnumerable<Comment> executeQuery(string sql)
		{
			sql += " limit " + Limit + " offset " + Offset + " ;";
			return DataMapper.Query(new MySqlCommand(sql));
		}*/

	}

}
