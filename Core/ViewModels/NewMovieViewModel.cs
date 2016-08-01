using E_LearningWeb.Validators;
using System.ComponentModel.DataAnnotations;
using E_LearningWeb.Models;

namespace E_LearningWeb.ViewModels
{
    public class NewMovieViewModel
    {
        public int Id { get; set; }
        public int CourseId { get; set; }

        [StringLength(60, MinimumLength = 5)]
        [Required]
        public string Title { get; set; }

        [Required]
        [YoutubeUrl]
        public string VideoUrl { get; set; }

        public NewMovieViewModel(Movie movie)
        {
            Id = movie.Id;
            CourseId = movie.CourseId;
            Title = movie.Title;
            VideoUrl = movie.VideoUrl;
        }

        public NewMovieViewModel()
        {
        }
    }
}