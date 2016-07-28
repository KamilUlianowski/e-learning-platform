using E_LearningWeb.Models;
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
        }

        public ElearningDbContext ELearningContext
        {
            get { return Context as ElearningDbContext; }
        }
    }
}