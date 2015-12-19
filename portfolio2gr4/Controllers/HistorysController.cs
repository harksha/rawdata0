
﻿using DAL.Rewrittable;
using System;
using portfolio2gr4.Models;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Routing;
using DAL;

namespace portfolio2gr4.Controllers
{
	public class HistorysController : BaseApiController
	{
		
		private HistoryRepository _hisRepository = new HistoryRepository(ConfigurationManager.ConnectionStrings["remote"].ConnectionString);


		/*	static HistoryMapper dataMapper = new HistoryMapper(ConfigurationManager.ConnectionStrings["remote"].ConnectionString);
			HistoryRepository _hisRepository = new HistoryRepository(dataMapper);

			public IEnumerable<HistoryModel> Get()
			{
				var helper = new UrlHelper(Request);
				return _hisRepository.GetAll()
					.Select(history => ModelFactory.Create(history));
			}*/

		public IEnumerable<HistoryModel> Get(int uid)
		{
			var helper = new UrlHelper(Request);
			return _hisRepository.GetByUserId(uid).Select(vote => ModelFactory.Create(vote));
		}
		public IEnumerable<HistoryModel> GetBySearchName(string searchText_History)
		{
			var helper = new UrlHelper(Request);// I don't need url helper here 
			return _hisRepository.GetByFullTextSearch(searchText_History, "body", 1000, 0).Select(annotation => ModelFactory.Create(annotation));
		}
		/*public HttpResponseMessage Post([FromBody] HistoryModel model)
		{
			var helper = new UrlHelper(Request);
			var history = ModelFactory.Parse(model);
			_hisRepository.Insert(history);
			return Request.CreateResponse(
				HttpStatusCode.Created
				, ModelFactory.Create(history));
		}*/

	}
}
