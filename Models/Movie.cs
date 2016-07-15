﻿using System.ComponentModel.DataAnnotations;

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

        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string Title { get; set; }

        [Required]
        public string VideoUrl { get; set; }
    }
}