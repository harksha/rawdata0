using Microsoft.VisualStudio.TestTools.UnitTesting;
using DAL.ReadOnly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ReadOnly.Tests
{
	[TestClass()]
	public class AnswerRepositoryTests
	{
		private AnswerRepository answerRepository;
        private void initilizeTest()
		{
			string connectionString = "server=wt-220.ruc.dk;database=raw4;uid=raw4;pwd=raw4";
			 answerRepository = new AnswerRepository(connectionString);
		}
		/// <summary>
		/// only test limit number.
		/// </summary>
		[TestMethod()]
		public void getAllTest()
		{
			initilizeTest();
			int limit = 3;
			var answers = answerRepository.GetAll(limit);
			Assert.AreEqual(limit, answers.Count());
        }

		/// <summary>
		/// given id = 271436
		/// 
		/// parentid = 263952;
		/// </summary>
		[TestMethod()]
		public void GetByIdTest()
		{
			initilizeTest();
			int expected = 263952;
			var answer = answerRepository.GetById(271436);
			int actual = answer.ParentId;
            Assert.AreEqual(expected, actual);
		}

		
		


	}
}