using E_LearningWeb.Services;
using E_LearningWeb.ViewModels;

namespace E_LearningWeb.Models
{
    public class Movie
    {
        public double SumOfVotes { get; set; }

        public double Rating
        {
            get
            {
                if (NumberOfVotes == 0)
                    return 0;
                return SumOfVotes / NumberOfVotes;
            }
        }
        public int NumberOfVotes { get; set; }
        public int Id { get; set; }
        public int CourseId { get; set; }
        public string Title { get; set; }
        public string VideoUrl { get; set; }

        public Movie(CourseViewModel courseViewModel)
        {
            CourseId = courseViewModel.NewMovie.CourseId;
            VideoUrl = "https://www.youtube.com/embed/" +
                       DataConversionService.GetVideoId(courseViewModel.NewMovie.VideoUrl);
            Title = courseViewModel.NewMovie.Title;
        }

        public Movie()
        {
        }
    }
}