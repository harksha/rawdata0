using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace portfolio2gr4.Models
{
	public class VoteModel
	{
		public string Url { get; set; }
		public int VoteType { get; set; }
		public DateTime Date { get; set; }
		public int UserId { get; set; }
		public int PostId { get; set; }
	}
}