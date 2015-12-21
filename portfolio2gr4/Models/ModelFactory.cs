using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Routing;
using DAL;
using portfolio2gr4.Models;
using DAL.ReadOnly;
using DAL.Rewrittable;

namespace WebService.Models
{
	public class ModelFactory
	{

		private UrlHelper _urlHelper;

		// create the url helper with the injected request
		public ModelFactory(HttpRequestMessage request)
		{
			_urlHelper = new UrlHelper(request);
		}

		public UserModel Create(User user)
		{
			return new UserModel
			{
				Url = _urlHelper.Link("UserByIdApi", new { Id = user.Id }), //HERE
				Name = user.Name,
				WebsiteUrl = user.WebsiteUrl,
				Reputation = user.Reputation,
				CreationDate = user.CreationDate,
				Age = user.Age,
				UpVotes = user.UpVotes,
				DownVotes = user.DownVotes,
				Location = user.Location
			};
		}

		public User Parse(UserModel model)
		{
			return new User
			{
				Name = model.Name,
				WebsiteUrl = model.WebsiteUrl,
				Reputation = model.Reputation,
				CreationDate = model.CreationDate,
				Age = model.Age,
				UpVotes = model.UpVotes,
				DownVotes = model.DownVotes,
				Location = model.Location
			};
		}

		public QuestionModel Create(Question question )
		{ 
			return new QuestionModel
			{
				Url = _urlHelper.Link("QuestionByIdApi", new { id = question.Id }), 
				Body = question.Body,
				Title = question.Title,
				Score = question.Score,
				CreationDate = question.CreationDate,
				Owner = question.Owner
			};
		}

		public AnswerModel Create(Answer answer)
		{
			return new AnswerModel
			{
				Url = _urlHelper.Link("AnswerByIdApi", new { id = answer.Id }),
				Body = answer.Body,
				Score = answer.Score,
				CreationDate = answer.CreationDate,
				Owner = answer.Owner,
				ParentId = answer.ParentId
			};
		}

		
		public CommentModel Create(Comment comment)
		{
			return new CommentModel
			{
				//postId = comment.PostId,
				//Url = _urlHelper.Link("QuestionCommentApi", new { id = comment.Id }),
				postId = comment.PostId,
				creationDate = comment.CreationDate,
				text = comment.Text,
				//userid = comment.Userid
			};
		}

		public PostModel Create(Post post)
		{
			return new PostModel
			{
				Url = _urlHelper.Link("PostByIdApi", new { id = post.Id }),
				Body = post.Body,
				Score = post.Score
			};
		}
		public Post Parse(PostModel model)
		{
			return new Post
			{
				Body = model.Body,
				Score = model.Score
			};
		}

		public VoteModel Create(Vote vote)
		{
			return new VoteModel
			{
				VoteType = vote.VoteType,
				PostId = vote.PostId,
				UserId = vote.UserId,
				Date = vote.Date
			};
		}

		public AnnotationModel Create(Annotation annotation)
		{
			return new AnnotationModel
			{
				Url = _urlHelper.Link("AnnotationByIdApi", new { id = annotation.Id }),
				Body = annotation.Body,
				Date = annotation.Date,
				UserId = annotation.UserId,
				PostId = annotation.PostId

			};
		}

		public Vote Parse(VoteModel model)
		{
			return new Vote
			{
				UserId = model.UserId,
				PostId = model.PostId,
				Date = DateTime.Now,
				VoteType = model.VoteType
			};
		}
		public Annotation Parse(AnnotationModel model)
		{
			return new Annotation
			{
				Body = model.Body,
				Date = model.Date,
				UserId = model.UserId,
				PostId = model.PostId
			};
		}

		public HistoryModel Create(History history)
		{
			return new HistoryModel
			{
				Url = _urlHelper.Link("HistoryByIdApi", new { id = history.Id }),
				Body = history.Body,
				Date = history.Date,
				UserId = history.UserId

			};
		}
		public History Parse(HistoryModel model)
		{
			return new History
			{
				Body = model.Body,
				Date = model.Date,
				UserId = model.UserId,

			};
		}
	}
}