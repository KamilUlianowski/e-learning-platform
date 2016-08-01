using E_LearningWeb.Models;
using E_LearningWeb.ViewModels;
using System.Collections.Generic;

namespace E_LearningWeb.Repositories
{
    public interface IMovieRepository : IRepository<Movie>
    {
        void AddVote(int movieId, double rating);
        void UpdateMovie(NewMovieViewModel movie);
        void DeleteMovie(int id);
        void AddMovie(CourseViewModel courseViewModel);
        Movie GetSingleMovie(int id);
        IEnumerable<Movie> GetMoviesFromCourse(int courseId);
    }
}