using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using E_LearningWeb.Models;

namespace E_LearningWeb.Services
{
    public interface ITestService
    {
        List<Question> GetQuestions();
    }
}