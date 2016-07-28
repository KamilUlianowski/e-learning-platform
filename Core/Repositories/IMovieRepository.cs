﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using E_LearningWeb.Models;

namespace E_LearningWeb.Repositories
{
    public interface IMovieRepository : IRepository<Movie>
    {
        void AddVote(int movieId, double rating);
    }
}