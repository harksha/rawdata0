using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace portfolio2gr4.Models
{
	public class CommentModel
	{

		//public string Url { get; set; }
		public int postId { get; set; }
		public string text { get; set; }
		public string creationDate { get; set; }
		public string UserId { get; set; }
	}
}