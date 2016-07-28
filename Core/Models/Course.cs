using System;
using System.Collections.Generic;
using System.Data.Entity;
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
        public string Path { get; set; }
        public int NumberOfMovies { get; set; }
        public string ShortDescription { get; set; }
        public string Author { get; set; }
    }
}