namespace E_LearningWeb.Repositories
{
    public interface IUnitOfWork
    {
        ICourseRepository Courses { get; }
        ITestResultsRepository TestResults { get; }
        IMovieRepository Movies { get; }
        int Complete();
    }
}