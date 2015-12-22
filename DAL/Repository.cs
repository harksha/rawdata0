using DAL.Rewrittable;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
	public abstract class Repository<T> : IRepository<T> where T : class, IIdentityField
	{
		public IDataMapper<T> DataMapper { get; set; }

		public IUpdatableDataMapper<T> UpdatableDataMapper { get; set; }

		public Repository(IDataMapper<T> dataMapper)
		{
			DataMapper = dataMapper;
		}
		public Repository(IUpdatableDataMapper<T> updatabledatamapper)
		{
			UpdatableDataMapper = updatabledatamapper;
		}
		public virtual T GetById(int id)
		{
			if (DataMapper == null)
			{
				return UpdatableDataMapper.GetById(id);
			}
			else
			{
				return DataMapper.GetById(id);
			}

		}


		/// <summary>
		/// To Ionana , I think this method shouold be moved to questions repository class or atleast you should change method name 
		/// Panpan suggested here . 
		/// </summary>
		/// <param name="key"></param>
		/// <param name="limit"></param>
		/// <param name="offset"></param>
		/// <returns></returns>
		public IEnumerable<T> GetAllQuestionsByKey(string key, int limit = 10, int offset = 0)
		{
			string[] stringSeparators = new string[] { " ", "," };
			string[] words = key.Split(stringSeparators, StringSplitOptions.None);
			string[] parsedWords = words.Select(word => "'%" + word + "%'").ToArray();
			var sqlWhere = "WHERE body like " + parsedWords[0];
			var w_list = new List<string>(parsedWords);
			w_list.RemoveAt(0);
			parsedWords = w_list.Select(word => "AND body like " + word).ToArray();

			var sql = string.Format("SELECT ID, {0} FROM {1} {2} {5} LIMIT {3} OFFSET {4}",
					string.Join(", ", DataMapper.Attributes),
					DataMapper.TableName,
					sqlWhere,
					limit,
					offset,
					string.Join(" ", parsedWords)
			);
			return DataMapper.Query(new MySqlCommand(sql));
		}

		public virtual IEnumerable<T> GetByKeyWords(string key, string column, int limit = 10, int offset = 0)
		{
			string[] stringSeparators = new string[] { " ", "," };
			string[] words = key.Split(stringSeparators, StringSplitOptions.None);
			string[] parsedWords = words.Select(word => "'%" + word + "%'").ToArray();
			var sqlWhere = "WHERE " + column + " like " + parsedWords[0];
			var w_list = new List<string>(parsedWords);
			w_list.RemoveAt(0);// why ?
			parsedWords = w_list.Select(word => "AND " + column + " like " + word).ToArray();

			var sql = string.Format("SELECT ID, {0} FROM {1} {2} {5} LIMIT {3} OFFSET {4}",
					string.Join(", ", DataMapper.Attributes),
					DataMapper.TableName,
					sqlWhere,
					limit,
					offset,
					string.Join(" ", parsedWords)
			);
			return DataMapper.Query(new MySqlCommand(sql));
		}

		public IEnumerable<T> GetAll(int limit = 10, int offset = 0)
		{
			if (DataMapper == null)
			{
				var sql = string.Format("SELECT ID, {0} FROM {1} LIMIT {2} OFFSET {3}",
				string.Join(", ", UpdatableDataMapper.Attributes),
				UpdatableDataMapper.TableName,
				limit,
				offset);
				return UpdatableDataMapper.Query(new MySqlCommand(sql));
			}
			else
			{
				//somehow we need to choose between DataMapper and UpdatableDataMapper
				var sql = string.Format("SELECT ID, {0} FROM {1} LIMIT {2} OFFSET {3}",
					string.Join(", ", DataMapper.Attributes),
					DataMapper.TableName,
					limit,
					offset);
				return DataMapper.Query(new MySqlCommand(sql));
			}
		}
		public IEnumerable<T> GetAll(int uid, int limit = 10, int offset = 0)
		{
			IDataMapper<T> mapper;
			if (DataMapper == null)
			{
				mapper = UpdatableDataMapper;
			}
			else
			{
				mapper = DataMapper;
			}
			var sql = string.Format("SELECT ID, {0} FROM {1} where userId = {4} LIMIT {2} OFFSET {3}",
				string.Join(", ", mapper.Attributes),
				mapper.TableName,
				limit,
				offset,
				uid);
			return mapper.Query(new MySqlCommand(sql));

		}

		public IEnumerable<T> GetByPost(int postid, int limit = 10, int offset = 0)
		{
			if (DataMapper == null)
			{
				var sql = string.Format("SELECT ID, {0} FROM {1} WHERE postId={4} LIMIT {2} OFFSET {3} ",
				string.Join(", ", UpdatableDataMapper.Attributes),
				UpdatableDataMapper.TableName,
				limit,
				offset,
				postid);
				return UpdatableDataMapper.Query(new MySqlCommand(sql));
			}
			else
			{

				var sql = string.Format("SELECT ID, {0} FROM {1} WHERE postId={4} LIMIT {2} OFFSET {3} ",
				string.Join(", ", DataMapper.Attributes),
				DataMapper.TableName,
				limit,
				offset,
				postid);
				return DataMapper.Query(new MySqlCommand(sql));
			}
		}

		public IEnumerable<T> GetByUserId(int userid, int limit = 10, int offset = 0)
		{

			var sql = string.Format("SELECT ID, {0} FROM {1} WHERE userId={4} LIMIT {2} OFFSET {3} ",
			string.Join(", ", UpdatableDataMapper.Attributes),
			UpdatableDataMapper.TableName,
			limit,
			offset,
			userid);
			return UpdatableDataMapper.Query(new MySqlCommand(sql));

		}

		public IEnumerable<T> GetAllQuestions(int limit, int offset)
		{
			var sql = string.Format("SELECT ID, {0} FROM {1} WHERE postTypeID=1 order by creationDate DESC LIMIT {2} OFFSET {3}  ",
				string.Join(", ", DataMapper.Attributes),
				DataMapper.TableName,
				limit,
				offset);
			return DataMapper.Query(new MySqlCommand(sql));
		}
		public IEnumerable<T> GetAllAnswers(int qid, int limit = 10, int offset = 0)
		{
			var sql = string.Format("SELECT ID, {0} FROM {1} WHERE postTypeID=2 AND parentQuestionID={4} LIMIT {2} OFFSET {3} ",
				string.Join(", ", DataMapper.Attributes),
				DataMapper.TableName,
				limit,
				offset,
				qid);
			return DataMapper.Query(new MySqlCommand(sql));
		}
		/// <summary>
		/// a search solution use full text search 
		/// </summary>
		/// <param name="searchText">the text received from searchbox </param>
		/// <param name="columns">the column in database</param>
		/// <param name="limit">the records number that will be returned</param>
		/// <param name="offset">start point resultset</param>
		/// <param name="orderby">the column that is used to be ordered by</param>
		/// <param name="ASC_DESC">ordering method</param>
		/// <returns></returns>
		public virtual IEnumerable<T> GetByFullTextSearch(string searchText, string columns, int limit = 10, int offset = 0, string orderby = "relevance", string ASC_DESC = "DESC")
		{
			IDataMapper<T> mapper;
			if (DataMapper == null)
			{
				mapper = UpdatableDataMapper;
			}
			else
			{
				mapper = DataMapper;
			}
			var condition = "where match (" + columns + ") " + "against( '" + searchText + "') ORDER BY " + orderby + " " + ASC_DESC;
			var sql = string.Format("SELECT ID, {0}{1} FROM {2} {3} limit {4} offset {5} ",
				string.Join(", ", mapper.Attributes),
				",match (" + columns + ")" + "against ('" + searchText + "') AS relevance",
				mapper.TableName,
				condition,
				limit,
				offset
				);
			return mapper.Query(new MySqlCommand(sql));
		}
		public IEnumerable<T> GetByKeyWords(string key, string column, int UserID, int limit = 10, int offset = 0)
		{
			IDataMapper<T> mapper;
			if (DataMapper == null)
			{
				mapper = UpdatableDataMapper;
				mapper = (IUpdatableDataMapper<T>)mapper;
			}
			else
			{
				mapper = DataMapper;
			}
			string[] stringSeparators = new string[] { " ", "," };
			string[] words = key.Split(stringSeparators, StringSplitOptions.None);
			string[] parsedWords = words.Select(word => "'%" + word + "%'").ToArray();
			var sqlWhere = "WHERE " + column + " like " + parsedWords[0];
			var w_list = new List<string>(parsedWords);
			w_list.RemoveAt(0);
			parsedWords = w_list.Select(word => "AND " + column + " like " + word).ToArray();
			var sql = string.Format("SELECT ID, {0} FROM {1} {2} {5} AND userID = {6} LIMIT {3} OFFSET {4}",
					string.Join(", ", mapper.Attributes),//0
					mapper.TableName,//1
					sqlWhere,//2
					limit,//3
					offset,//4
					parsedWords.Length == 0 ? "" : string.Join(" ", parsedWords),//5
					UserID//6
			);
			return mapper.Query(new MySqlCommand(sql));
		}
	}
}
