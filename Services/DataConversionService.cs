using E_LearningWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace E_LearningWeb.Services
{
    public class DataConversionService
    {
        public static List<Question> GetIncorrectQuestions(string incorrectQuestionId, List<Question> questions)
        {
            try
            {
                var incorrectQuestoins = new List<Question>();
                var numbers = incorrectQuestionId.Split(',').Select(Int32.Parse).ToList();
                for (int i = 0; i < numbers.Count; i++)
                {
                    questions.First(x => x.Id == numbers[i]).SelectedAnswer = numbers[i + 1];
                    incorrectQuestoins.Add(questions.First(x => x.Id == numbers[i]));
                    i++;
                }
                return incorrectQuestoins;
            }
            catch (Exception)
            {
                return new List<Question>();
            }
        }

        public static string GetVideoId(string link)
        {
            var youtubeMatch =
             new Regex(@"youtu(?:\.be|be\.com)/(?:.*v(?:/|=)|(?:.*/)?)([a-zA-Z0-9-_]+)")
             .Match(link);
            return youtubeMatch.Success ? youtubeMatch.Groups[1].Value : string.Empty;
        }

        public static List<Course> CountMoviesInCourse(IEnumerable<Course> courses, IEnumerable<Movie> movies)
        {

            foreach (var item in movies)
            {
                var firstOrDefault = courses.FirstOrDefault(x => x.Id == item.CourseId);
                if (firstOrDefault != null)
                    firstOrDefault.NumberOfMovies++;
            }
            return courses.ToList(); // Zwraca kursy z ich liczbą filmów
        }
    }
}