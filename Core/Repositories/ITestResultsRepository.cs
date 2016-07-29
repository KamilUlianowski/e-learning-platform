using E_LearningWeb.Models;

namespace E_LearningWeb.Repositories
{
    public interface ITestResultsRepository : IRepository<TestResult>
    {
        int GetNumberOfCorrectAnswers(int userId);
    }
}