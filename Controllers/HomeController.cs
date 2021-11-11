﻿using InsuranceDLL.DataAccess.DomainModels;
using InsuranceDLL.DataAccess.Interface;
using InuranceAssignmentAPD03.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace InuranceAssignmentAPD03.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IInsuranceDB db;

        public HomeController(ILogger<HomeController> logger,IInsuranceDB db)
        {
            _logger = logger;
            this.db = db;
        }

        [HttpGet]
        public IActionResult ViewAllTransactions() { 

            return View(db.GetAllTransactions()); 
        }

        public IActionResult Index()
        {
            var Users = db.GetAllUsers();

            string UserName = User.Identity.Name;

            var matchedEmployees = Users.FirstOrDefault(m => m.UserId == UserName || m.Email == UserName);

            if (matchedEmployees == null || matchedEmployees == new InsuranceDLL.DataAccess.DomainModels.User())
            {
                User model = new User();

                model.UserId = Guid.NewGuid().ToString();
                model.Email = User.Identity.Name;

                db.AddUser(model);

            matchedEmployees = Users.FirstOrDefault(m => m.UserId == UserName || m.Email == UserName);
            }


            var matchedaccounts = db.GetAllAccounts().FirstOrDefault(m => m.UserId == matchedEmployees.UserId);

            if (matchedaccounts == null || matchedaccounts == new InsuranceDLL.DataAccess.DomainModels.Account())
            {
                Account model = new Account();

                model.AccountId = Guid.NewGuid().ToString();
                model.UserId = matchedEmployees.UserId;
                

                db.AddAccount(model);

            }

            List<PostViewModel> posts = new List<PostViewModel>();

            posts.Add(new PostViewModel { Image = "Policy01.jpg", Link = "https://google.com", Text = "New Insurance Policy Death Cover!", Title = "Death Cover" });
            posts.Add(new PostViewModel { Image = "Policy01.jpg", Link = "https://google.com", Text = "New Accidental Death Cover !", Title = "Accident Cover" });
            posts.Add(new PostViewModel { Image = "Add01.jpg", Link = "https://google.com", Text = "Now Desease Cover !", Title = "Disease Cover" });
            posts.Add(new PostViewModel { Image = "StalkImage.png", Link = "https://google.com", Text = "New Insurance Policy 4 your Family!", Title = "Family Cover" });


            return View(posts);
        }

        public IActionResult Privacy()
        {
            return View();
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
