
using DAL.Rewrittable;
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


		//static HistoryMapper dataMapper = new HistoryMapper(ConfigurationManager.ConnectionStrings["remote"].ConnectionString);
		//HistoryRepository _hisRepository = new HistoryRepository(dataMapper);

		public HttpResponseMessage Get(int uid, int size = 10, int page = 1)
		{
			page--;
			var helper = new UrlHelper(Request);
			if (size > 100) size = 10;
			int offset = page * size;
			int next_page = page;
			int prev_page = page;
			//need to count max pages from total results and implement that
			if (page < 1) { prev_page = 0; } else { prev_page--; }
			next_page++;

			var prev = helper.Link("HistoryApi", new { size = size, page = prev_page, uid = uid }).ToString();
			var next = helper.Link("HistoryApi", new { size = size, page = next_page, uid = uid });
			var response = Request
				.CreateResponse(
				HttpStatusCode.OK,
				_hisRepository
				.GetAll(uid, size, offset)
				.Select(history => ModelFactory.Create(history)))
				;
			response.Headers.Add("next-page", next);
			response.Headers.Add("prev-page", prev);
			return response;
		}

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
		public HttpResponseMessage GetBySearch(string searchText_History, int uid, int size = 10, int page = 1)
		{
			page--;
			var helper = new UrlHelper(Request);
			if (size > 100) size = 10;
			int offset = page * size;
			int next_page = page;
			int prev_page = page;
			//need to count max pages from total results and implement that
			if (page < 1) { prev_page = 0; } else { prev_page--; }
			next_page++;

			var prev = helper.Link("HistorySearchByKey", new { size = size, page = prev_page ,searchText_History=searchText_History,uid=uid}).ToString();
			var next = helper.Link("HistorySearchByKey", new { size = size, page = prev_page, searchText_History = searchText_History, uid = uid });
			var response = Request
				.CreateResponse(
				HttpStatusCode.OK,
				_hisRepository.GetByKeyWords(searchText_History, "body", uid, size, offset).Select(history => ModelFactory.Create(history)));
			response.Headers.Add("next-page", next);
			response.Headers.Add("prev-page", prev);
			return response;
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
