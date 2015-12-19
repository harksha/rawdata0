using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Rewrittable
{
	public abstract class UpdatableDataMapper<T> : DataMapper<T>, IUpdatableDataMapper<T> where T : class, IIdentityField
	{
		public UpdatableDataMapper(string connectionString) : base(connectionString) { }
		public abstract int Insert(T entity);
		public abstract int Update(T entity);
		public abstract T GetByPostAndUser(int postid, int userid);

		protected int NextId()// purpose ?
		{
			using (var connection = new MySqlConnection(ConnectionString))
			{
				connection.Open();
				var cmd = new MySqlCommand("select max(id) from " + TableName, connection);
				var id = cmd.ExecuteScalar();
				if (id != null)
				{
					return int.Parse(id.ToString()) + 1;
				}

				return 1;
			}
		}


	}
}
