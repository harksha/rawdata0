using System;

namespace DAL.ReadOnly
{
	public class Comment : IIdentityField
	{
		public int Id { get; set; }
		public int PostId { get; set; }
		public string Text { get; set; }
		public string CreationDate { get; set; }
		public int UserId { get; set; }
	}
}
