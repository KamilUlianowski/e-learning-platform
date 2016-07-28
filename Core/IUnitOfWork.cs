using E_LearningWeb.Core.Repositories;

namespace E_LearningWeb.Repositories
{
    public interface IUnitOfWork
    {
        ICourseRepository Courses { get; }
        ITestResultsRepository TestResults { get; }
        IMovieRepository Movies { get; }
        IQuestionRepository Questions { get; }
        int Complete();
    }
}