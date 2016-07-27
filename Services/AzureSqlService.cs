using E_LearningWeb.Models;
using E_LearningWeb.ViewModels;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace E_LearningWeb.Services
{
    public class AzureSqlService : IAzureSqlService
    {
        public List<Course> GetAllCourses()
        {
            using (var ctx = new ElearningDbContext())
            {
                var courses = ctx.Courses.ToList();
                return courses;
            }
        }

        public List<Movie> GetAllMovies()
        {
            using (var ctx = new ElearningDbContext())
            {
                var movies = ctx.Movies.ToList();
                return movies;
            }
        }

        public List<TestResult> GetTestsResults(int userId)
        {
            using (var ctx = new ElearningDbContext())
            {
                var testResults = ctx.TestResults.ToList();
                return testResults;
            }
        }

        public List<Question> GetQuestions(int courseId)
        {
            using (var ctx = new ElearningDbContext())
            {
                var questions = ctx.Questions.Include(x => x.Answers).ToList();
                return questions;
            }
        }

        public List<Movie> GetMoviesFromCourse(int courseId)
        {
            using (var ctx = new ElearningDbContext())
            {
                var movies = ctx.Movies.ToList().Where(x => x.CourseId == courseId).ToList();
                return movies;
            }
        }

        public Course GetCourse(int id)
        {
            using (var ctx = new ElearningDbContext())
            {
                var course = ctx.Courses.FirstOrDefault(x => x.Id == id);
                return course;
            }
        }

        public bool AddMovie(NewMovieViewModel movie)
        {
            Movie newMovie = new Movie()
            {
                CourseId = movie.CourseId,
                Title = movie.Title,
                VideoUrl = movie.VideoUrl
            };
            using (var ctx = new ElearningDbContext())
            {
                ctx.Movies.Add(newMovie);
                ctx.SaveChanges();
                return true;
            }

        }

        public bool AddVote(int movieId, double rating)
        {
            using (var ctx = new ElearningDbContext())
            {
                var movie = ctx.Movies.FirstOrDefault(x => x.Id == movieId);
                if (movie != null)
                {
                    movie.NumberOfVotes++;
                    movie.SumOfVotes += rating;
                }
                ctx.SaveChanges();

            }

            return true;
        }
    }
}