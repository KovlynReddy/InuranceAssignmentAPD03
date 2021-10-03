using InsuranceDLL.DataAccess.DomainModels;
using InsuranceDLL.DataAccess.Interface;
using InuranceAssignmentAPD03.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InuranceAssignmentAPD03.Controllers
{
    public class PolicyController : Controller
    {
        public IInsuranceDB Db { get; }
        public PolicyController(IInsuranceDB db)
        {
            Db = db;
        }


        [HttpGet]
        public IActionResult Index()
        {
            CreateProfileViewModel model = new CreateProfileViewModel();
           

            return View();
        }

        [HttpPost]
        public IActionResult Index(CreateProfileViewModel model) 
        {
            var myuser = Db.GetAllUsers().FirstOrDefault(m => m.Email == User.Identity.Name); ;
            Profile newProfile = new Profile();
            newProfile.UserName = model.UserName;
            newProfile.Age = model.Age;
            newProfile.DOB = model.DOB;
            newProfile.UserId = myuser.UserId;
            newProfile.FamilyMembers = model.FamilyMembers;
            newProfile.FamilyId = Guid.NewGuid().ToString();

             newProfile.Sinus                   = model.ASinus                       ? 1 : 0   ;
             newProfile.HeridataryDeseases      = model.AHeridataryDeseases          ? 1 : 0   ;
             newProfile.Diabeties               = model.ADiabeties                   ? 1 : 0   ;
             newProfile.Cancer                  = model.ACancer                      ? 1 : 0   ;
             newProfile.TerminalIllnesses       = model.ATerminalIllnesses           ? 1 : 0   ;
             newProfile.Kids                    = model.AKids                        ? 1 : 0   ;
             newProfile.DangerousWorkingEnviroment     = model.ADangerousWorkingEnviroment  ? 1 : 0   ;
             newProfile.TravelForWork           = model.ATravelForWork               ? 1 : 0   ;
             newProfile.SmokeCigerrets          = model.ASmokeCigerrets              ? 1 : 0   ;
             newProfile.DrinkAlcohol            = model.ADrinkAlcohol                ? 1 : 0   ;
             newProfile.OnPercesciptiveDrugs     = model.AOnPercesciptiveDrugs        ? 1 : 0   ;
            newProfile.OnCronicDrugs = model.AOnCronicDrugs ? 1 : 0;

            Db.AddProfile(newProfile);

            return RedirectToAction("Index","Home");
        }
        [HttpGet]
        public IActionResult CreatePolicy() {

            CreatePolicyViewModel model = new CreatePolicyViewModel();
            
            return View(model); 
        }        
        
        [HttpPost]
        public IActionResult CreatePolicy(CreatePolicyViewModel model) {

            Policy newpolicy = new Policy();
            newpolicy.PolicyName = model.PolicyName;
            newpolicy.BaseCost = model.BaseCost;
            newpolicy.Constant = model.Constant;
            newpolicy.Type = model.Type;
            newpolicy.PolicyId = Guid.NewGuid().ToString();
            newpolicy.AverageAge = model.AverageAge;
            newpolicy.PolicyDescription = model.PolicyDescription;

            Db.AddPolicy(newpolicy);
            
            
            return View(model); 
        }

        [HttpPost]
        public IActionResult ApplyForPolicy(string id) {
            var selectedpolicy = Db.GetAllPolicys().FirstOrDefault(m => m.PolicyId == id);
            var selecteduser = Db.GetAllUsers().FirstOrDefault(m => m.Email == User.Identity.Name);
            var selectedprofile = Db.GetAllProfiles().FirstOrDefault(m => m.UserId == selecteduser.UserId);
            var selectedaccount = Db.GetAllAccounts().FirstOrDefault(m => m.UserId == selecteduser.UserId);

            Transaction apply = new Transaction();

            apply.AccountId = selectedaccount.AccountId;
            apply.Amount = selectedpolicy.BaseCost +( Math.Abs(selectedpolicy.AverageAge - selectedprofile.Age) * 10) + (10 *( selectedprofile.Sinus == 1 ? 1 :2 ))  +(10 * (selectedprofile.HeridataryDeseases == 1 ? 1 : 2))+(10 * (selectedprofile.Diabeties == 1 ? 1 : 2))+(10 * (selectedprofile.Cancer == 1 ? 1 : 2))+(10 * (selectedprofile.TerminalIllnesses == 1 ? 1 : 2))+(10 * (selectedprofile.Kids == 1 ? 1 : 2))+(10 * (selectedprofile.DangerousWorkingEnviroment == 1 ? 1 : 2))+(10 * (selectedprofile.TravelForWork == 1 ? 1 : 2))+(10 * (selectedprofile.SmokeCigerrets == 1 ? 1 : 2))+(10 * (selectedprofile.DrinkAlcohol == 1 ? 1 : 2))+(10 * (selectedprofile.OnPercesciptiveDrugs == 1 ? 1 : 2))+(10 * (selectedprofile.OnCronicDrugs == 1 ? 1 : 2)) ;
            apply.PolicyId = selectedpolicy.PolicyId ;
            apply.Notes = "Application";
            apply.ProfileId = selectedprofile.ProfileId ;
            apply.TimeSent = DateTime.Now;
            apply.TransactionId = Guid.NewGuid().ToString() ;
            apply.UserId = selecteduser.UserId;

            Db.AddTransaction(apply);
            

            return View(); }

        [HttpGet]
        public IActionResult ApproveApplication(string id) {

            var application = Db.GetAllTransactions().FirstOrDefault(m=>m.TransactionId == id);

            application.Notes = "Approved";

            // send email 

            Db.UpdateTransaction(application);

            return RedirectToAction("Index","Home");

        }        
        
        [HttpGet]
        public IActionResult DenyApplication(string id) {

            var application = Db.GetAllTransactions().FirstOrDefault(m=>m.TransactionId == id);

            application.Notes = "Approved";

            return RedirectToAction("Index","Home");

        }

        [HttpGet]
        public IActionResult ViewAllApplications() {

            var applications = Db.GetAllTransactions().Where(m => m.Notes == "Application").ToList();

            var applicants = Db.GetAllUsers();

            applicants = applicants.Where(m=> applications.Select(g=>g.UserId).ToList().Contains(m.UserId)).ToList();
            var selectedpolicies = Db.GetAllPolicys().Where(m=> applications.Select(g=>g.PolicyId).ToList().Contains(m.PolicyId)).ToList();

            ViewAllApplicantionsViewModel model = new ViewAllApplicantionsViewModel();
            model.Applications = applications;
            model.Applicants = applicants;
            model.SelectedPolicies = selectedpolicies;

            for(int i = 0; i < model.Applications.Count; i++)
{
                if (model.SelectedPolicies[i] != null&&model.Applications[i] != null&&model.Applicants[i] != null)
                {

                model.ApplicationModels.Add(new ApplicationViewModel(model.SelectedPolicies[i],model.Applications[i],model.Applicants[i]));
                
                }

            }

            return View(model);

        }

        [HttpGet]
        public IActionResult ViewPolicy(string id) {
            var selectedpolicy = Db.GetAllPolicys().FirstOrDefault(m=>m.PolicyId == id);

            return View(selectedpolicy); }

        [HttpGet]
        public IActionResult ViewAllPolicies() {
            ViewAllPoliciesViewModel model = new ViewAllPoliciesViewModel();
            model.Policies = Db.GetAllPolicys();



            return View(model); }
        [HttpPost]
        public IActionResult ViewAllPolicies(ViewAllPoliciesViewModel model) { return View(); }
    }
}
