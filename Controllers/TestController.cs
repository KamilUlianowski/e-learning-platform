﻿using E_LearningWeb.Models;
using E_LearningWeb.Services;
using E_LearningWeb.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace E_LearningWeb.Controllers
{
    public class TestController : Controller
    {
        private readonly ISharepointService _sharepointService;
        private List<Question> _questions;
        private static string _answers;

        public TestController(ISharepointService sharepointService)
        {
            _sharepointService = sharepointService;
        }

        public ActionResult Index(int courseId)
        {
            TestViewModel testViewModel = new TestViewModel();
            testViewModel.ListOfQuestions = _sharepointService.GetQuestions(courseId);
            _questions = testViewModel.ListOfQuestions;
            return View(testViewModel);
        }

        public ActionResult Result(string correctAnswers, string incorrectAnswers, int courseId)
        {
            var testResult = new TestResult()
            {
                CourseId = courseId,
                Result = correctAnswers,
                UserId = _sharepointService.GetUserId(),
                CourseName = _sharepointService.GetCourse(courseId).Title
            };

            _sharepointService.AddResultOfTest(testResult);
            _questions = _sharepointService.GetQuestions(courseId);
            return View(new TestViewModel()
            {
                CorrectAnswers = correctAnswers,
                ListOfQuestions = GetIncorrectQuestions(incorrectAnswers)
            });
        }

        public ActionResult ResultHistory()
        {
            var results = _sharepointService.GetTestsResults(_sharepointService.GetUserId());
            return View(results);
        }

        public void SaveAnswers(string answers)
        {
            _answers = answers;
        }

        public List<Question> GetIncorrectQuestions(string incorrectQuestionId)
        {
            try
            {
                var incorrectQuestoins = new List<Question>();
                var numbers = incorrectQuestionId.Split(',').Select(Int32.Parse).ToList();
                for (int i = 0; i < numbers.Count; i++)
                {
                    _questions.First(x => x.Id == numbers[i]).SelectedAnswer = numbers[i + 1];
                    incorrectQuestoins.Add(_questions.First(x => x.Id == numbers[i]));
                    i++;
                }
                return incorrectQuestoins;
            }
            catch (Exception)
            {
                return new List<Question>();
            }
        }

    }
}