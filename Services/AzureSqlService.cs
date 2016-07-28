using E_LearningWeb.Models;
using E_LearningWeb.ViewModels;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace E_LearningWeb.Services
{
    public class AzureSqlService : IAzureSqlService
    {
        public IEnumerable<Course> GetAllCourses()
        {
            using (var ctx = new ElearningDbContext())
            {
                var courses = ctx.Courses.ToList();
                return courses;
            }
        }

        public IEnumerable<Movie> GetAllMovies()
        {
            using (var ctx = new ElearningDbContext())
            {
                var movies = ctx.Movies.ToList();
                return movies;
            }
        }

        public IEnumerable<TestResult> GetTestsResults(int userId)
        {
            using (var ctx = new ElearningDbContext())
            {
                var testResults = ctx.TestResults.ToList();
                return testResults;
            }
        }

        public IList<Question> GetQuestions(int courseId)
        {
            using (var ctx = new ElearningDbContext())
            {
                var questions = ctx.Questions.Include(x => x.Answers).Where(x => x.CourseId == courseId).ToList();
                return questions;
            }
        }

        public IEnumerable<Movie> GetMoviesFromCourse(int courseId)
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

        public Movie GetSingleMovie(int id)
        {
            using (var ctx = new ElearningDbContext())
            {
                var movie = ctx.Movies.FirstOrDefault(x => x.Id == id);
                return movie;
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

        public bool DeleteMovie(int movieId)
        {
            using (var ctx = new ElearningDbContext())
            {
                var movie = ctx.Movies.FirstOrDefault(x => x.Id == movieId);
                if (movie != null)
                {
                    ctx.Movies.Remove(movie);
                    ctx.SaveChanges();
                    return true;
                }
                return false;
            }
        }

        public bool UpdateMovie(NewMovieViewModel newMovie)
        {
            using (var ctx = new ElearningDbContext())
            {
                var updateMovie = ctx.Movies.FirstOrDefault(x => x.Id == newMovie.Id);
                if (updateMovie != null)
                {
                    updateMovie.CourseId = newMovie.CourseId;
                    updateMovie.Title = newMovie.Title;
                    updateMovie.VideoUrl = newMovie.VideoUrl;
                    ctx.SaveChanges();
                    return true;
                }
            }
            return false;
        }

        public bool AddResultOfTest(TestResult testResult)
        {
            using (var ctx = new ElearningDbContext())
            {
                ctx.TestResults.Add(testResult);
                ctx.SaveChanges();
                return true;
            }
        }
    }
}