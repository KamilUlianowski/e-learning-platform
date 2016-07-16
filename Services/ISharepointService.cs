using E_LearningWeb.Models;
using System.Collections.Generic;

namespace E_LearningWeb.Services
{
    public interface ISharepointService
    {
        List<Course> GetCourses();
        List<Post> GetDiscussionPosts(string courseId);
        List<Movie> GetMovies(int courseId);
        Movie GetMovieInfo(int id);
        bool AddVote(int movieId, double rating);
        bool DeleteMovie(int id);
        bool UpdateMovie(Movie movie);
        bool AddMovie(Movie movie);
        bool AddPost(string text, string courseId);

    }
}