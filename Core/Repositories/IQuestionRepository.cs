using E_LearningWeb.Models;
using E_LearningWeb.Repositories;
using System.Collections.Generic;

namespace E_LearningWeb.Core.Repositories
{
    public interface IQuestionRepository : IRepository<Question>
    {
        IEnumerable<Question> GetQuestionsWithAnswers(int courseId);
    }
}