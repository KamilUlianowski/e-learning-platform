﻿using E_LearningWeb.Models;
using System.Collections.Generic;

namespace E_LearningWeb.ViewModels
{
    public class CourseViewModel
    {
        public List<Movie> ListOfMovies { get; set; }
        public int CourseId { get; set; }
        public CourseViewModel()
        {
            ListOfMovies = new List<Movie>();
        }

    }
}