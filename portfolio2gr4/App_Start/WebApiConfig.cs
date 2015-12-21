using System.Web.Http;

namespace portfolio2gr4
{
	public static class WebApiConfig
	{
		public static void Register(HttpConfiguration config)
		{

			config.MapHttpAttributeRoutes();
			config.Routes.MapHttpRoute(
				name: "UserApi",
				routeTemplate: "api/users/{size}-{page}",
				defaults: new { controller = "Users"}
				);
			config.Routes.MapHttpRoute(
				name: "UserByIdApi",
				routeTemplate: "api/users/{id}",
				defaults: new { controller = "Users"}
				);
			config.Routes.MapHttpRoute(
				name: "UserSearchApi",
				routeTemplate: "api/users/GetBySearchName/{searchText_Name}-{size}-{page}",
				defaults: new { controller = "Users" }
			);
			//users - End
			config.Routes.MapHttpRoute(
				name: "QuestionApi",
				routeTemplate: "api/questions/{size}-{page}",
				defaults: new { controller = "Questions" }
			);
			config.Routes.MapHttpRoute(
				name: "QuestionByIdApi",
				routeTemplate: "api/questions/{id}",
				defaults: new { controller = "Questions" }
			);
			// this api is not used 
			config.Routes.MapHttpRoute(
				name: "QuestionKeywordApi",
				routeTemplate: "api/questions/GetByKey/{keywords}",
				defaults: new { controller = "Questions" }
			);
			config.Routes.MapHttpRoute(
				name: "QuestionSearchTitle",
				routeTemplate: "api/questions/search_title/{searchText_title}",
				defaults: new { controller = "Questions" }
			);
			config.Routes.MapHttpRoute(
				name: "QuestionSearchApi",
				routeTemplate: "api/questions/search/{searchText}-{size}-{page}",
				defaults: new { controller = "Questions" }
			);
			// questions - End
			config.Routes.MapHttpRoute(
				name: "AnswerApi",
				routeTemplate: "api/questions/{qid}/answers",
				defaults: new { controller = "Answers" }
			);

			config.Routes.MapHttpRoute(
				name: "QuestionCommentApi",
				routeTemplate: "api/questions/{pid}/comments",
				defaults: new { controller = "Comments", pid = RouteParameter.Optional }
			);

			config.Routes.MapHttpRoute(
				name: "AnswersCommentApi",
				routeTemplate: "api/questions/{qid}/answers/{pid}/comments",
				defaults: new { controller = "Comments", qid = RouteParameter.Optional }
			);

			config.Routes.MapHttpRoute(
				name: "QuestionVotesApi",
				routeTemplate: "api/questions/{pid}/votes",
				defaults: new { controller = "Votes" }
			);
			config.Routes.MapHttpRoute(
				name: "AnswerVotesApi",
				routeTemplate: "api/questions/{qid}/answers/{pid}/votes",
				defaults: new { controller = "Votes" }
			);
			config.Routes.MapHttpRoute(
				name: "AnnotationApi",
				routeTemplate: "api/annotations/{size}-{page}",
				defaults: new { controller = "Annotations" }
				);
			config.Routes.MapHttpRoute(
				name: "AnnotationByIdApi",
				routeTemplate: "api/annotations/{id}",
				defaults: new { controller = "Annotations" }
				);
			config.Routes.MapHttpRoute(
				name: "AnnotationSearchApi",
				routeTemplate: "api/annotations/search/{searchText_Annotation}-{uid}-{size}-{page}",
				defaults: new { controller = "Annotations" }
			);
			config.Routes.MapHttpRoute(
				name: "AnnotationApiByUserAndPost",
				routeTemplate: "api/annotations/{postid}/{userid}",
				defaults: new { controller = "Annotations" }
			);
			
            config.Routes.MapHttpRoute(
			   name: "HistoryApi",
			   routeTemplate: "api/users/{uid}-{size}-{page}/historys",
			   //routeTemplate: "api/historys/{id}",
			   defaults: new { controller = "Historys", }
			  );
			config.Routes.MapHttpRoute(
			   name: "HistoryByIdApi",
			   routeTemplate: "api/historys/{id}",
			   defaults: new { controller = "Historys", }
			  );
			config.Routes.MapHttpRoute(
			   name: "HistorySearchByKey",
			   routeTemplate: "api/historys/search/{searchText_history}-{uid}-{size}-{page}",
			   //routeTemplate: "api/historys/{id}",
			   defaults: new { controller = "Historys", }
			  );

			config.Routes.MapHttpRoute(
				name: "DefaultApi",
				routeTemplate: "api/{controller}/{id}",
				defaults: new { attribute = RouteParameter.Optional, id = RouteParameter.Optional }
			);
		}
	}
}
