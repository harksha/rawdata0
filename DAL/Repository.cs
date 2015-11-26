﻿using DAL.Rewrittable;
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

		public IEnumerable<T> GetByPost(int postid, int limit = 10, int offset = 0) {
			var sql = string.Format("SELECT ID, {0} FROM {1} WHERE postId={4} LIMIT {2} OFFSET {3} ",
				string.Join(", ", UpdatableDataMapper.Attributes),
				UpdatableDataMapper.TableName,
				limit,
				offset,
				postid);
			return UpdatableDataMapper.Query(new MySqlCommand(sql));
		}

		public IEnumerable<T> GetAllQuestions( int limit = 10, int offset = 0)
		{
			var sql = string.Format("SELECT ID, {0} FROM {1} WHERE postTypeID=1 LIMIT {2} OFFSET {3} ",
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


	}
}
