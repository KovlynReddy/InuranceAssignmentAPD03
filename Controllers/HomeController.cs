using InsuranceDLL.DataAccess.DomainModels;
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

            }


            var matchedaccounts = db.GetAllAccounts().FirstOrDefault(m => m.UserId == matchedEmployees.UserId);

            if (matchedaccounts == null || matchedaccounts == new InsuranceDLL.DataAccess.DomainModels.Account())
            {
                Account model = new Account();

                model.AccountId = Guid.NewGuid().ToString();
                model.UserId = matchedEmployees.UserId;
                

                db.AddAccount(model);

            }

            return View();
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
