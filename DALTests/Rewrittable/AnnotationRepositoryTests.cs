using Microsoft.VisualStudio.TestTools.UnitTesting;
using DAL.Rewrittable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Rewrittable.Tests
{
	[TestClass()]
	public class AnnotationRepositoryTests
	{
		AnnotationRepository _annoRepository;
		private void initiaizeTest()
		{
			string connectionString = "server=wt-220.ruc.dk;database=raw4;uid=raw4;pwd=raw4";
			_annoRepository = new AnnotationRepository(connectionString);
		}
		/// <summary>
		/// given id = 6;
		/// should have userid = 1 and postid = 7664;
		/// </summary>
		[TestMethod()]
		public void getByIdTest()
		{
			initiaizeTest();
			var annotation = _annoRepository.GetById(6);
			Assert.AreEqual(1, annotation.UserId);
			Assert.AreEqual(7664, annotation.PostId);
		}
		/// <summary>
		/// given userid = 72 and postid = 105975
		///  should have id = 4
		/// </summary>
		[TestMethod()]
		public void getByPostAndUserTest()
		{
			initiaizeTest();
			var annotation = _annoRepository.GetByPostAndUser(105975, 72);

			var actual = new Annotation { Id=4,PostId= 105975,UserId=72,Body="0",Date= DateTime.Parse("2011-10-04 01:01:00") };
			Assert.AreEqual(annotation.Id, actual.Id);
			Assert.AreEqual(annotation.Body, actual.Body);
			Assert.AreEqual(annotation.PostId, actual.PostId);
			Assert.AreEqual(annotation.UserId, actual.UserId);
			Assert.AreEqual(annotation.Date, actual.Date);

		}

	

		[TestMethod()]
		public void InsertTest()
		{
			initiaizeTest();
			Random rdm = new Random();
			int id = rdm.Next(10000);
			int postid = rdm.Next(10000);
			int userid = rdm.Next(10000);
			Annotation expected = new Annotation
			{
				Id = id,
				UserId = userid,
				PostId = postid,
				Body = "annotation unit test , postid = " + postid + " userid = " + userid,
				Date = DateTime.Now
			};
			int count = _annoRepository.Insert(expected);
			Annotation actual = _annoRepository.GetByPostAndUser(postid, userid);
			Assert.IsTrue(expected.PostId == actual.PostId);
			Assert.IsTrue(expected.UserId == actual.UserId);
		}

		/// <summary>
		/// given annotation with userid = 72 , postid = 105975 that saved in database before test
		/// </summary>
		[TestMethod()]
		public void UpdationTest()
		{
			initiaizeTest();
			Random rdm = new Random();
			Annotation a = _annoRepository.GetByPostAndUser(105975, 72);
            var body = a.Body;
			a.Body = "new unit test with a random number = " + rdm.Next(100000);
			lock (_annoRepository) { int count = _annoRepository.Updation(a); }
			initiaizeTest();
            Annotation actual = _annoRepository.GetByPostAndUser(105975, 72);
			Assert.AreEqual(a.Body, actual.Body);
		}

		//[TestMethod()]
		//public void GetByPostAndUserTest()
		//{
		//	Assert.Fail();
		//}
	}
}