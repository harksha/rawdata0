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
		public IEnumerable<UserModel> Get()
		{
			var helper = new UrlHelper(Request);
			return _userRepository.GetAll()
				.Select(user => ModelFactory.Create(user));
		}
		public IEnumerable<UserModel> GetBySearchName(string searchText_Name)
		{
			var helper = new UrlHelper(Request);// I don't need url helper here 
			return _userRepository.GetByKeyWords(searchText_Name, "displayName",10,0).Select(user => ModelFactory.Create(user)); ;
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