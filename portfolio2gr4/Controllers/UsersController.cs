using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Routing;
using WebService.Models;
using DAL;
using portfolio2gr4.Models;
using System.Configuration;

namespace portfolio2gr4.Controllers
{
	public class UsersController : BaseApiController
	{
		private UserRepository _userRepository = new UserRepository(ConfigurationManager.ConnectionStrings["remote"].ConnectionString);
		public HttpResponseMessage Get(int size, int page)
		{
			
			var helper = new UrlHelper(Request);
			if (size > 100) size = 10;
			int offset = page * size;
			int next_page = page;
			int prev_page = page;
			//need to count max pages from total results and implement that
			if (page < 1) { prev_page = 0; } else { prev_page--; }
			next_page++;

			var prev = helper.Link("UserApi", new { size = size, page = prev_page }).ToString();
			var next = helper.Link("UserApi", new { size = size, page = next_page });
			var response = Request
				.CreateResponse(
				HttpStatusCode.OK,
				_userRepository
				.GetAll(size, offset)
				.Select(question => ModelFactory.Create(question)))
				;
			response.Headers.Add("next-page", next);
			response.Headers.Add("prev-page", prev);
			return response;
		}
		public HttpResponseMessage GetBySearchName(string searchText_Name,int size=10, int page=1)
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

			var prev = helper.Link("UserSearchApi", new { size = size, page = prev_page }).ToString();
			var next = helper.Link("UserSearchApi", new { size = size, page = next_page });
			var response = Request
				.CreateResponse(
				HttpStatusCode.OK,
				_userRepository.GetByKeyWords(searchText_Name, "displayName", 10, 0).Select(user => ModelFactory.Create(user)));
			response.Headers.Add("next-page", next);
			response.Headers.Add("prev-page", prev);
			return response;
			//return _userRepository.GetByFullTextSearch(searchText_Name, "displayName", 1000, 0).Select(user => ModelFactory.Create(user));
		}

		public HttpResponseMessage GetById(int id)
		{
			var helper = new UrlHelper(Request);
			var user = _userRepository.GetById(id);
			if (user == null)
			{
				return Request.CreateResponse(HttpStatusCode.NotFound);
			}
			return Request.CreateResponse(
				HttpStatusCode.OK, ModelFactory.Create(user));
		}

	}
}