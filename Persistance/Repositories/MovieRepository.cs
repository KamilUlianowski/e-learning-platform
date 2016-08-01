using E_LearningWeb.Models;
using E_LearningWeb.ViewModels;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace E_LearningWeb.Repositories
{
    public class MovieRepository : Repository<Movie>, IMovieRepository
    {
        public MovieRepository(DbContext context) : base(context)
        {
        }

        public void AddVote(int movieId, double rating)
        {
            var selectedMovie = ELearningContext.Movies.SingleOrDefault(x => x.Id == movieId);
            if (selectedMovie == null) return;
            selectedMovie.SumOfVotes += rating;
            selectedMovie.NumberOfVotes++;
            ELearningContext.SaveChanges();
        }

        public void UpdateMovie(NewMovieViewModel movie)
        {
            var movieToUpdate = ELearningContext.Movies.FirstOrDefault(x => x.Id == movie.Id);
            if (movieToUpdate == null) return;

            movieToUpdate.CourseId = movie.CourseId;
            movieToUpdate.VideoUrl = movie.VideoUrl;
            movieToUpdate.Title = movie.Title;
            ELearningContext.SaveChanges();
        }

        public void DeleteMovie(int id)
        {
            var movieToDelete = ELearningContext.Movies.FirstOrDefault(x => x.Id == id);
            if (movieToDelete != null)
            {
                ELearningContext.Movies.Remove(movieToDelete);
            }
            ELearningContext.SaveChanges();
        }

        public void AddMovie(CourseViewModel courseViewModel)
        {
            var newMovie = new Movie(courseViewModel);
            ELearningContext.Movies.Add(newMovie);
            ELearningContext.SaveChanges();
        }

        public Movie GetSingleMovie(int id)
        {
            return ELearningContext.Movies.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Movie> GetMoviesFromCourse(int courseId)
        {
            return ELearningContext.Movies.Where(x => x.CourseId == courseId);
        }

        public ElearningDbContext ELearningContext
        {
            get { return Context as ElearningDbContext; }
        }
    }
}