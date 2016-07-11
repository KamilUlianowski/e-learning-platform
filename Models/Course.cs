using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_LearningWeb.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
    }
}