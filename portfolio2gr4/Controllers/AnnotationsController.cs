
using DAL.Rewrittable;
using portfolio2gr4.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Routing;

namespace portfolio2gr4.Controllers
{
	public class AnnotationsController : BaseApiController
	{
		AnnotationRepository _annoRepository = new AnnotationRepository(ConfigurationManager.ConnectionStrings["remote"].ConnectionString);

		public HttpResponseMessage Get(int size=10, int page=1)
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

			var prev = helper.Link("AnnotationApi", new { size = size, page = prev_page }).ToString();
			var next = helper.Link("AnnotationApi", new { size = size, page = next_page });

			var response = Request
				.CreateResponse(
				HttpStatusCode.OK,
				_annoRepository
				.GetAll(size, offset)
				.Select(annotation => ModelFactory.Create(annotation)))
				;
			response.Headers.Add("next-page", next);
			response.Headers.Add("prev-page", prev);
			return response;
		}
		public HttpResponseMessage GetByPostAndUser(int postid, int userid)
		{
			var helper = new UrlHelper(Request);
			var annotation = _annoRepository.GetByPostAndUser(postid, userid);
			if (annotation == null)
			{
				return Request.CreateResponse(HttpStatusCode.NotFound);

			}
			return Request.CreateResponse(
				HttpStatusCode.OK
				, ModelFactory.Create(annotation));
		}

		public HttpResponseMessage GetById(int id)
		{
			var helper = new UrlHelper(Request);
			var annotation = _annoRepository.GetById(id);
			if (annotation == null)
			{
				return Request.CreateResponse(HttpStatusCode.NotFound);

			}
			return Request.CreateResponse(
				HttpStatusCode.OK
				, ModelFactory.Create(annotation));

		}
		public HttpResponseMessage Post([FromBody] AnnotationModel model)
		{
			var helper = new UrlHelper(Request);
			var annotation = ModelFactory.Parse(model);
			_annoRepository.Insert(annotation);
			return Request.CreateResponse(
				HttpStatusCode.Created
				, ModelFactory.Create(annotation));
		}


		public HttpResponseMessage Put(int id, [FromBody] AnnotationModel model)
		{
			var helper = new UrlHelper(Request);
			var annotation = ModelFactory.Parse(model);
			annotation.Id = id;
			_annoRepository.Updation(annotation);
			return Request.CreateResponse(HttpStatusCode.OK);
		}
		public HttpResponseMessage GetByKeyWords(string searchText_Annotation, int uid, int size=10, int page=1)
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

			var prev = helper.Link("AnnotationSearchApi", new { size = size, page = prev_page, searchText_Annotation = searchText_Annotation, uid = uid }).ToString();
			var next = helper.Link("AnnotationSearchApi", new { size = size, page = next_page, searchText_Annotation = searchText_Annotation, uid = uid  });

			var response = Request
				.CreateResponse(
				HttpStatusCode.OK,
				_annoRepository.GetByKeyWords(searchText_Annotation, "body", uid, size, offset).Select(annotation => ModelFactory.Create(annotation)));			
			response.Headers.Add("next-page", next);
			response.Headers.Add("prev-page", prev);
			return response;
		}
	}
}