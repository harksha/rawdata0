using DAL.Rewrittable;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace DAL
{
	public class HistoryRepository : Repository<History>
	{
		public HistoryRepository(IUpdatableDataMapper<History> updatabledatamapper) : base(updatabledatamapper)
		{
		}

		public HistoryRepository(string connectiomString) : base(new HistoryMapper(connectiomString)) { }

		public void Insert(History history)
		{
			UpdatableDataMapper.Insert(history);
		}

	}
}