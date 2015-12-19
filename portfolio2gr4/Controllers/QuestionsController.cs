using DAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Routing;
using WebService.Models;

namespace portfolio2gr4.Controllers
{
	public class QuestionsController : BaseApiController
	{		
		private QuestionRepository _questionRepository = new QuestionRepository(ConfigurationManager.ConnectionStrings["remote"].ConnectionString);
		public HttpResponseMessage Get(int size, int page) {
			var helper = new UrlHelper(Request);
			if (size > 100) size = 10;
			int offset = page * size;
			int next_page = page;
			int prev_page = page;
			//need to count max pages from total results and implement that
			if (page < 1) { prev_page = 0; } else { prev_page--; }
			next_page++;

			var prev = helper.Link("QuestionApi", new { size = size, page = prev_page }).ToString();
			var next = helper.Link("QuestionApi", new { size=size, page=next_page  });

			var response =  Request
				.CreateResponse(
				HttpStatusCode.OK,
				_questionRepository
				.GetAllQuestions(size, offset)
				.Select(question => ModelFactory.Create(question)))
				;
			response.Headers.Add("next-page", next);
			response.Headers.Add("prev-page", prev);
			return response;
		}

		[AcceptVerbs("GET", "HEAD")]
		public IEnumerable<QuestionModel> GetByKey(string keywords)
		{
			var helper = new UrlHelper(Request);
			return _questionRepository.GetAllQuestionsByKey(keywords).Select(question => ModelFactory.Create(question));
		}

		public IEnumerable<QuestionModel> GetBySearchTitle(string searchText_title)
		{
			var helper = new UrlHelper(Request);// I don't need url helper here 
			return _questionRepository.GetByFullTextSearch(searchText_title, "title", 1000, 0).Select(question => ModelFactory.Create(question));
		}

		public IEnumerable<QuestionModel> GetBySearch(string searchText)
		{
			var helper = new UrlHelper(Request);// I don't need url helper here 
			return _questionRepository.GetByFullTextSearch(searchText, "title,body", 10, 0).Select(question => ModelFactory.Create(question));
		}
		public HttpResponseMessage GetById(int id) {
			var helper = new UrlHelper(Request);
			var question = _questionRepository.GetById(id);
			if(question ==null)
			{
				return Request.CreateResponse(HttpStatusCode.NotFound);
			}
			return Request.CreateResponse(HttpStatusCode.OK, ModelFactory.Create(question));
		}
	}
}