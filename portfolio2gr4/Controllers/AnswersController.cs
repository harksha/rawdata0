using DAL.ReadOnly;
using portfolio2gr4.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Http.Routing;

namespace portfolio2gr4.Controllers
{
	public class AnswersController : BaseApiController
	{		
		private AnswerRepository _answerRepository = new AnswerRepository(ConfigurationManager.ConnectionStrings["remote"].ConnectionString);
		public IEnumerable<AnswerModel> Get(int qid)
		{
			var helper = new UrlHelper(Request);
			return _answerRepository.GetAllAnswers(qid).Select(answer => ModelFactory.Create(answer));
		}
		//public string Get(int qid) { return "fd"; }
	}
}