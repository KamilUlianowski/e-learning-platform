using E_LearningWeb.Models;
using E_LearningWeb.Repositories;
using E_LearningWeb.Services;
using System.Collections.Generic;
using System.Linq;

namespace E_LearningWeb.ViewModels
{
    public class CourseViewModel
    {
        public List<Movie> ListOfMovies { get; set; }
        public List<Post> ListOfPosts { get; set; }
        public List<Question> ListOfQuestions { get; set; }
        public Course SpecificCourse { get; set; }
        public NewMovieViewModel NewMovie { get; set; }
        public string Text { get; set; }
        private readonly UnitOfWork _unitOfWork = new UnitOfWork(new ElearningDbContext());
        private readonly ISharepointService _sharepointService = new SharepointService();

        public CourseViewModel(int courseId)
        {
            ListOfMovies = _unitOfWork.Movies.GetAll().ToList();
            ListOfQuestions = _unitOfWork.Questions.GetQuestionsWithAnswers(courseId).ToList();
            ListOfPosts = _sharepointService.GetDiscussionPosts(courseId).ToList();
            SpecificCourse = _unitOfWork.Courses.FirstOrDefault(x => x.Id == courseId);
            NewMovie = new NewMovieViewModel();
        }

        public CourseViewModel()
        {

        }

        public CourseViewModel(List<Post> posts, List<Movie> movies, List<Question> questions, Course course)
        {
            ListOfPosts = posts;
            ListOfMovies = movies;
            ListOfQuestions = questions;
            SpecificCourse = course;
        }

        public CourseViewModel(List<Movie> movies)
        {
            ListOfMovies = movies;
        }

        public CourseViewModel(List<Post> posts, Course course)
        {
            ListOfPosts = posts;
            SpecificCourse = course;
        }

    }
}