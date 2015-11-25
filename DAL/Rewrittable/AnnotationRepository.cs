﻿using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Configuration;
using System;
using DAL.ReadOnly;

namespace DAL.Rewrittable
{
    public class AnnotationRepository : Repository<Annotation>
    {
        public AnnotationRepository(IUpdatableDataMapper<Annotation> dataMapper) : base(dataMapper) { }

        public void Insert(Annotation annotation)
        {
            UpdatableDataMapper.Insert(annotation);
        }

        public void Updation(Annotation annotation)
        {
            UpdatableDataMapper.Update(annotation);

        }

<<<<<<< HEAD
		public Annotation GetByPostAndUser(int postid, int userid)
		{
			return new Annotation { };
		}
	}
=======
    }
>>>>>>> 5dd3b2745ca244f5ae8659a35a2e882e0560e844
}