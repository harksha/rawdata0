﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL 
{
	public class QuestionRepository : Repository<Question>
	{
		public QuestionRepository(IDataMapper<Question> dataMapper) : base(dataMapper) { }
		
	}
}