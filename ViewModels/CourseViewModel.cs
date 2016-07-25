using E_LearningWeb.Models;
using System.Collections.Generic;

namespace E_LearningWeb.ViewModels
{
    public class CourseViewModel
    {
        public List<Movie> ListOfMovies { get; set; }
        public List<Post> ListOfPosts { get; set; }
        public List<Question> ListOfQuestions { get; set; }
        public Course SpecificCourse { get; set; }
        public Movie NewMovie { get; set; }
        public string Text { get; set; }
        public CourseViewModel()
        {
            ListOfMovies = new List<Movie>();
            ListOfPosts = new List<Post>();
            NewMovie = new Movie();
        }

    }
}