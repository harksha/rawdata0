using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Rewrittable
{
	public interface IUpdatableDataMapper<T> : IDataMapper<T> where T : class, IIdentityField
	{
		int Insert(T entity);
		int Update(T entity);
		T GetByPostAndUser(int p, int u);
	}
}
