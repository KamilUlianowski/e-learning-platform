using E_LearningWeb.Models;
using System;
using System.Data.Entity;
using System.Linq;

namespace E_LearningWeb.Repositories
{
    public class TestResultsRepository : Repository<TestResult>, ITestResultsRepository
    {
        public TestResultsRepository(DbContext context) : base(context)
        {
        }

        public int GetNumberOfCorrectAnswers(int userId)
        {
            var results = ELearningContext.TestResults.Where(x => x.UserId == userId).ToList();
            var correctAnswers = 0;
            foreach (var item in results)
            {
                correctAnswers += Int32.Parse(item.Result[0].ToString());
            }
            return correctAnswers;
        }

        public ElearningDbContext ELearningContext
        {
            get { return Context as ElearningDbContext; }
        }
    }
}