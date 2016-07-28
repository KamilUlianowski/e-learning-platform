using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using E_LearningWeb.Models;

namespace E_LearningWeb.Repositories
{
    public class CourseRepository : Repository<Course>, ICourseRepository
    {
        public CourseRepository(DbContext context) : base(context)
        {

        }
    }
}