using E_LearningWeb.Models;
using E_LearningWeb.ViewModels;
using System.Collections.Generic;

namespace E_LearningWeb.Services
{
    public interface IAzureSqlService
    {
        List<Course> GetAllCourses();
        List<Movie> GetAllMovies();
        List<TestResult> GetTestsResults(int userId);
        List<Question> GetQuestions(int courseId);
        List<Movie> GetMoviesFromCourse(int courseId);
        Course GetCourse(int id);
        bool AddMovie(NewMovieViewModel movie);
        bool AddVote(int movieId, double rating);

    }
}