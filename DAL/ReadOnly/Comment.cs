using System;

namespace DAL.ReadOnly
{
	public class Comment : IIdentityField
	{
		public int Id { get; set; }
		public int userId { get; set; }
		public int PostId { get; set; }
		public string Text { get; set; }
		public DateTime CreationDate { get; set; }
		
	}
}
