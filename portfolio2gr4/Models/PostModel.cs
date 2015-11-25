﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace portfolio2gr4.Models
{
	public class PostModel
	{
		public string Owner { get; set; }
		public string Url { get; set; }
		public DateTime CreationDate { get; set; }
		public int Score { get; set; }
		public string Body { get; set; }
		
	}
}