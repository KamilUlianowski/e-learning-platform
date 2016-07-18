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
        private static List<Question> _questions;

        public TestController(ISharepointService sharepointService)
        {
            _sharepointService = sharepointService;
        }

        public ActionResult Index()
        {
            TestViewModel testViewModel = new TestViewModel();
            testViewModel.ListOfQuestions = _sharepointService.GetQuestions();
            _questions = testViewModel.ListOfQuestions;
            return View(testViewModel);
        }

        public ActionResult Result(string correctAnswers, string incorrectAnswersId)
        {

            GetIncorrectQuestions(incorrectAnswersId);
            return View(new TestViewModel()
            {
                CorrectAnswers = correctAnswers,
                ListOfQuestions = GetIncorrectQuestions(incorrectAnswersId)
            });
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
            catch (Exception ex)
            {
                RedirectToAction("Index", "Test");
                return new List<Question>();
            }
        }

    }
}