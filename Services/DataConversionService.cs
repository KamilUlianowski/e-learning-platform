using E_LearningWeb.Core.Models;
using E_LearningWeb.Models;
using E_LearningWeb.Repositories;
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
                    var q = questions.First(x => x.Id == numbers[i]);
                    q.SelectedAnswer = numbers[i + 1];

                    incorrectQuestoins.Add(q);
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

        public static List<User> GetCorrectAnswerForEachUser(List<User> users)
        {
            UnitOfWork unitOfWork = new UnitOfWork(new ElearningDbContext());
            foreach (var user in users)
            {
                user.CorrectAnswers = unitOfWork.TestResults.GetNumberOfCorrectAnswers(user.Id);
            }
            return users;
        }
    }
}