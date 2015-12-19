
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

		public IEnumerable<AnnotationModel> Get()
		{
			var helper = new UrlHelper(Request);
			return _annoRepository.GetAll()
				.Select(annotation => ModelFactory.Create(annotation));
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
		public IEnumerable<AnnotationModel> GetByKeyWords(string searchText_Annotation)
		{
			return _annoRepository.GetByKeyWords(searchText_Annotation,"body",4,1000,0).Select(annotation => ModelFactory.Create(annotation));
		}
	}
}