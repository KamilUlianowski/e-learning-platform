using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_LearningWeb.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public string Title { get; set; }
        public string VideoUrl { get; set; }
    }
}