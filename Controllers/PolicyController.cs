using FluentEmail.Core;
using FluentEmail.Smtp;
using InsuranceDLL.DataAccess.DomainModels;
using InsuranceDLL.DataAccess.Interface;
using InuranceAssignmentAPD03.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Mail;
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

            var SelectedUser = Db.GetAllUsers().FirstOrDefault(m => m.Email == User.Identity.Name);
            var SelectedProfile = Db.GetAllProfiles().FirstOrDefault(m=>m.UserId == SelectedUser.UserId);

            if (SelectedProfile != null && SelectedProfile != new Profile())
            {
                model.FamilyId = SelectedProfile.FamilyId;
            }

            return View(model);
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
            if (model.FamilyId == null || model.FamilyId == string.Empty)
            {
                newProfile.FamilyId = Guid.NewGuid().ToString();

            }
            else { newProfile.FamilyId = model.FamilyId; }

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
        public IActionResult CreateApeal(string id) {
            var apeal = Db.GetAllClaims().FirstOrDefault(m=>m.ClaimId==id);

            CreateApealViewModel model = new CreateApealViewModel();
            model.ClaimId = id;
            model.ApealNotes = string.Empty;
            model.ProfileId = apeal.ProfileId;
            model.PolicyId = apeal.PolicyId;
            model.Reason = string.Empty;

            return View(model); }

        [HttpPost]
        public IActionResult CreateApeal(CreateApealViewModel apeal) {

            var trans = Db.GetAllTransactions().FirstOrDefault(m=>m.ProfileId == apeal.ProfileId && m.PolicyId == apeal.PolicyId);
            var claim = Db.GetAllClaims().FirstOrDefault(m=>m.ClaimId == apeal.ClaimId);

            trans.Notes = "ApealUnApproved";
            claim.Notes = "ApealUnApproved";
            claim.Description = apeal.ApealNotes; 

            Db.UpdateClaim(claim);
            Db.UpdateTransaction(trans);

            return RedirectToAction("Index"); }

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

        [HttpGet]
        public IActionResult ViewAllMyClaims() {

            var alltransactions = Db.GetAllTransactions();
            var allclaims = Db.GetAllClaims();
            var allusers = Db.GetAllUsers();
            var allprofiles = Db.GetAllProfiles();
            var allaccounts = Db.GetAllAccounts();
            var allpolicies = Db.GetAllPolicys(); 

            var selecteduser = allusers.FirstOrDefault(m => m.Email == User.Identity.Name);
            var selectedprofile = allprofiles.FirstOrDefault(m => m.UserId == selecteduser.UserId);
            var selectedaccount = allaccounts.FirstOrDefault(m => m.UserId == selecteduser.UserId);
            var myclaims = allclaims.Where(m=>m.UserId == selecteduser.UserId || m.AccountId == selectedaccount.AccountId).ToList();

            List<ClaimViewModel> model = new List<ClaimViewModel>();



            foreach (var claim in myclaims)
            {

                var us = allusers.FirstOrDefault(m=>claim.UserId == m.UserId);
                var cla = allclaims.FirstOrDefault(m=>claim.ClaimId == m.ClaimId);
                var acc = allaccounts.FirstOrDefault(m=>us.UserId == m.UserId);
                var pr = allprofiles.FirstOrDefault(m=>us.UserId == m.UserId);
                var pol = allpolicies.FirstOrDefault(m => m.PolicyId == claim.PolicyId); 
                var tra = alltransactions.FirstOrDefault(m=>(m.UserId == us.UserId && m.PolicyId == pol.PolicyId )|| m.ClaimId == cla.ClaimId) ;
                model.Add(new ClaimViewModel(tra,cla,us,pr,acc,pol));
            }

            return View(model); 
        }

        [HttpGet]
        public IActionResult GenerateQoute(string policyid,string userid)
        { return View(); }

            [HttpPost]
        public IActionResult GenerateQoute(ViewPolicyViewModel policy)
        {
            var selectedpolicy = Db.GetAllPolicys().FirstOrDefault(m => m.PolicyId == policy.policy.PolicyId);
            var selecteduser = Db.GetAllUsers().FirstOrDefault(m => m.Email == User.Identity.Name);
            var selectedprofile = Db.GetAllProfiles().FirstOrDefault(m => m.ProfileId == policy.ProfileId);
            var selectedaccount = Db.GetAllAccounts().FirstOrDefault(m => m.UserId == selecteduser.UserId);

            Transaction apply = new Transaction();

            apply.AccountId = selectedaccount.AccountId;
            apply.Amount = selectedpolicy.BaseCost + (Math.Abs(selectedpolicy.AverageAge - selectedprofile.Age) * 10) + (10 * (selectedprofile.Sinus == 1 ? 1 : 2)) + (10 * (selectedprofile.HeridataryDeseases == 1 ? 1 : 2)) + (10 * (selectedprofile.Diabeties == 1 ? 1 : 2)) + (10 * (selectedprofile.Cancer == 1 ? 1 : 2)) + (10 * (selectedprofile.TerminalIllnesses == 1 ? 1 : 2)) + (10 * (selectedprofile.Kids == 1 ? 1 : 2)) + (10 * (selectedprofile.DangerousWorkingEnviroment == 1 ? 1 : 2)) + (10 * (selectedprofile.TravelForWork == 1 ? 1 : 2)) + (10 * (selectedprofile.SmokeCigerrets == 1 ? 1 : 2)) + (10 * (selectedprofile.DrinkAlcohol == 1 ? 1 : 2)) + (10 * (selectedprofile.OnPercesciptiveDrugs == 1 ? 1 : 2)) + (10 * (selectedprofile.OnCronicDrugs == 1 ? 1 : 2));
            apply.PolicyId = selectedpolicy.PolicyId;
            apply.Notes = "Application";
            apply.ProfileId = selectedprofile.ProfileId;
            apply.TimeSent = DateTime.Now;
            apply.TransactionId = Guid.NewGuid().ToString();
            apply.UserId = selecteduser.UserId;

            //model.SelectedPolicies = selectedpolicy;
            //model.Applicants = selecteduser;
            //model.Applications = apply;

            ApplicationViewModel model = new ApplicationViewModel(selectedpolicy,apply,selecteduser); 
            
            return View("ViewQoute",model);
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
            ViewPolicyViewModel model = new ViewPolicyViewModel();
              model.policy =  selectedpolicy ;
           
            var selectedUser = Db.GetAllUsers().FirstOrDefault(m=>m.Email == User.Identity.Name);
            var selectedProfile = Db.GetAllProfiles().FirstOrDefault(m=>m.UserId == selectedUser.UserId);


            if (selectedUser != null)
            {
            model.profiles = Db.GetAllProfiles().Where(m=>m.FamilyId == selectedProfile.FamilyId).ToList();
            }

            return View(model); }

        [HttpGet]
        public IActionResult ViewAllPolicies() {
            ViewAllPoliciesViewModel model = new ViewAllPoliciesViewModel();
            model.Policies = Db.GetAllPolicys();



            return View(model); }
        [HttpPost]
        public IActionResult ViewAllPolicies(ViewAllPoliciesViewModel model) { return View(); }

        private static void SendCompletedCallback(object sender, AsyncCompletedEventArgs e)
        {
            // Get the unique identifier for this asynchronous operation.
            String token = (string)e.UserState;

            if (e.Cancelled)
            {
                Console.WriteLine("[{0}] Send canceled.", token);
            }
            if (e.Error != null)
            {
                Console.WriteLine("[{0}] {1}", token, e.Error.ToString());
            }
            else
            {
                Console.WriteLine("Message sent.");
            }

        }
        public async Task SendMail(string amessage, string address)
        {

            #region mail1

            SmtpClient client = new SmtpClient("smtp.gmail.com");
            client.Credentials = new System.Net.NetworkCredential("Techno Solutions01", "T3$0em01");
            client.Port = 587;
            client.EnableSsl = true;
            // Specify the email sender.
            // Create a mailing address that includes a UTF8 character
            // in the display name.
            MailAddress from = new MailAddress("TechnoSolutions0001@gmail.com",
               "Techno" + (char)0xD8 + "Solutions01", System.Text.Encoding.UTF8);
            // Set destinations for the email message.
            MailAddress to = new MailAddress(address);
            // Specify the message content.

            MailMessage message = new MailMessage(from, to);

            message.Body = " " + amessage;
            // Include some non-ASCII characters in body and subject.
            string someArrows = new string(new char[] { '\u2190', '\u2191', '\u2192', '\u2193' });
            message.Body += Environment.NewLine + someArrows;
            message.BodyEncoding = System.Text.Encoding.UTF8;
            message.Subject = "Policy Payment " + someArrows;
            message.SubjectEncoding = System.Text.Encoding.UTF8;
            // Set the method that is called back when the send operation ends.
            client.SendCompleted += new SendCompletedEventHandler(SendCompletedCallback);
            // The userState can be any object that allows your callback
            // method to identify this send operation.
            // For this example, the userToken is a string constant.


            //client.Send(message);

            #endregion

            #region Mail2

            var sender = new SmtpSender(() => new SmtpClient(host: "smtp.gmail.com")
            {
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Port = 587,
                Credentials = new NetworkCredential("TechnoSolutions0001@gmail.com", "T3$0em01")
            });

            Email.DefaultSender = sender;

            var email = await Email
                .From(emailAddress: "TechnoSolutions0001@gmail.com")
                .To(emailAddress: address, name: "Hi User")
                .Subject(subject: " " + amessage)
                .Body(body: "Policy Payment")
                .SendAsync();

            #endregion
        }
    }
}
