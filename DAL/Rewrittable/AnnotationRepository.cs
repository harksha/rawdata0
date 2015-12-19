using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Configuration;
using System;
using DAL.ReadOnly;

namespace DAL.Rewrittable
{
	public class AnnotationRepository : Repository<Annotation>
	{
		public AnnotationRepository(string connectionString) : base(new AnnotationMapper(connectionString)) { }

		public int Insert(Annotation annotation)
		{
			return UpdatableDataMapper.Insert(annotation);
		}

		public int Updation(Annotation annotation)
		{
			return UpdatableDataMapper.Update(annotation);
		}

		public Annotation GetByPostAndUser(int postid, int userid)
		{
			return UpdatableDataMapper.GetByPostAndUser(postid, userid);
		}


		public IEnumerable<Annotation> GetByKeyWords(string key, string column, int UserID,int limit = 10, int offset = 0)
		{
			string[] stringSeparators = new string[] { " ", "," };
			string[] words = key.Split(stringSeparators, StringSplitOptions.None);
			string[] parsedWords = words.Select(word => "'%" + word + "%'").ToArray();
			var sqlWhere = "WHERE " + column + " like " + parsedWords[0];
			var w_list = new List<string>(parsedWords);
			w_list.RemoveAt(0);
			parsedWords = w_list.Select(word => "AND " + column + " like " + word).ToArray();
			var sql = string.Format("SELECT ID, {0} FROM {1} {2} {5} AND userID = {6} LIMIT {3} OFFSET {4}",
					string.Join(", ", DataMapper.Attributes),//0
					DataMapper.TableName,//1
					sqlWhere,//2
					limit,//3
					offset,//4
					parsedWords.Length==0?"": string.Join(" ", parsedWords),//5
					UserID//6
			);
			return DataMapper.Query(new MySqlCommand(sql));
		}
	}
}