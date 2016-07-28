using E_LearningWeb.Core.Repositories;
using E_LearningWeb.Models;
using E_LearningWeb.Repositories;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace E_LearningWeb.Persistance.Repositories
{
    public class QuestionRepository : Repository<Question>, IQuestionRepository
    {

        public QuestionRepository(DbContext context) : base(context)
        {
        }

        public IEnumerable<Question> GetQuestionsWithAnswers(int courseId)
        {
            return ELearningContext.Questions.Include(x => x.Answers).Where(x => x.CourseId == courseId).ToList();
        }

        public ElearningDbContext ELearningContext
        {
            get { return Context as ElearningDbContext; }
        }
    }
}