using E_LearningWeb.Core.Repositories;
using E_LearningWeb.Models;
using E_LearningWeb.Persistance.Repositories;

namespace E_LearningWeb.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ElearningDbContext _context;
        public ICourseRepository Courses { get; private set; }
        public ITestResultsRepository TestResults { get; private set; }
        public IMovieRepository Movies { get; }
        public IQuestionRepository Questions { get; }

        public UnitOfWork(ElearningDbContext context)
        {
            _context = context;
            Questions = new QuestionRepository(context);
            Movies = new MovieRepository(context);
            Courses = new CourseRepository(context);
            TestResults = new TestResultsRepository(context);
        }


        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}