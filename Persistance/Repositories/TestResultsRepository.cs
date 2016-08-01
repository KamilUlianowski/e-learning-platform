using E_LearningWeb.Models;
using E_LearningWeb.Services;
using System.Data.Entity;
using System.Linq;

namespace E_LearningWeb.Repositories
{
    public class TestResultsRepository : Repository<TestResult>, ITestResultsRepository
    {
        private ISharepointService _sharepointService;
        public TestResultsRepository(DbContext context) : base(context)
        {
            _sharepointService = new SharepointService();
        }

        public int GetNumberOfCorrectAnswers(int userId)
        {
            var results = ELearningContext.TestResults.Where(x => x.UserId == userId).ToList();
            return results.Sum(item => int.Parse(item.Result[0].ToString()));
        }

        public void AddTestResult(string correctAnswers, int courseId)
        {
            var course = ELearningContext.Courses.FirstOrDefault(x => x.Id == courseId);
            if (course != null)
            {
                var testResult = new TestResult(correctAnswers, _sharepointService.GetUserId(), courseId, course.Title);
                ELearningContext.TestResults.Add(testResult);
                ELearningContext.SaveChanges();
            }
        }

        public ElearningDbContext ELearningContext
        {
            get { return Context as ElearningDbContext; }
        }
    }
}