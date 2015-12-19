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
using DAL.ReadOnly;

namespace portfolio2gr4.Controllers
{
	public class CommentsController : BaseApiController
	{
		private CommentRepository _commentRepository = new CommentRepository(ConfigurationManager.ConnectionStrings["remote"].ConnectionString);
		public IEnumerable<CommentModel> Get(int pid)
		{
			var helper = new UrlHelper(Request);
			return _commentRepository.GetByPost(pid).Select(comment => ModelFactory.Create(comment));
		}
	}
}
