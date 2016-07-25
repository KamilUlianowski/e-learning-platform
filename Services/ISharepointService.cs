using E_LearningWeb.Models;
using System;
using System.Collections.Generic;

namespace E_LearningWeb.Services
{
    public interface ISharepointService
    {
        List<Course> GetCourses();
        List<Post> GetDiscussionPosts(string courseId);
        List<Movie> GetAllMovies();
        List<Question> GetQuestions(int courseId);
        List<Movie> GetMoviesFromCourse(List<Movie> movies, int id);
        List<Course> CountMovies(List<Course> courses, List<Movie> movies);
        List<TestResult> GetTestsResults(int userId);
        Movie GetMovieInfo(int id);
        Course GetCourse(int id);
        int GetUserId();
        bool AddVote(int movieId, double rating);
        bool DeleteMovie(int id);
        bool UpdateMovie(Movie movie);
        bool AddMovie(Movie movie);
        bool AddPost(string text, string courseId);
        bool AddResultOfTest(TestResult testResult);


    }
}