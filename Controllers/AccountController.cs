using FluentEmail.Core;
using FluentEmail.Smtp;
using InsuranceDLL.DataAccess.DomainModels;
using InsuranceDLL.DataAccess.Interface;
using InuranceAssignmentAPD03.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Transactions;

namespace InuranceAssignmentAPD03.Controllers
{
    public class AccountController : Controller
    {
        private readonly IInsuranceDB db;

        public AccountController(IInsuranceDB db)
        {
            this.db = db;
        }
        // GET: AccountController
        public ActionResult Index()
        {
            return View();
        }

        // GET: AccountController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AccountController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AccountController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AccountController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AccountController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AccountController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        [HttpPost]
        public IActionResult DepositMoney(ProfileViewModel model) {

            InsuranceDLL.DataAccess.DomainModels.Transaction deposit = new InsuranceDLL.DataAccess.DomainModels.Transaction();
            deposit.AccountId = model.account.AccountId;
            deposit.Amount = model.depositAmount;
            deposit.ClaimId = "Test"; // change
            deposit.Notes = "Deposit";
            deposit.PolicyId = "Test"; // change
            deposit.ProfileId = model.profile.ProfileId;
            deposit.TimeSent = DateTime.Now;
            deposit.UserId = model.user.UserId;
            deposit.TransactionId = Guid.NewGuid().ToString();

            db.AddTransaction(deposit);

            return View(); }

        [HttpGet]
        public IActionResult ViewProfile(string id) {

            var user = db.GetAllUsers().FirstOrDefault(m => m.UserId == id);
            var profile = db.GetAllProfiles().FirstOrDefault(m => m.UserId == id);
            var account = db.GetAllAccounts().FirstOrDefault(m => m.UserId == id);

            var transactions = db.GetAllTransactions().Where(m => m.AccountId == account.AccountId).ToList();

            int total = 0;

            foreach (var item in transactions)
            {
                total += item.Amount;

            }
            account.Balance = total;
            db.UpdateAccount(account);

            ProfileViewModel model = new ProfileViewModel();
            model.user = user;
            model.profile = profile;
            model.account = account;

            return View();
        }
        [HttpGet]
        public IActionResult ViewMyProfile() {

            var user = db.GetAllUsers().FirstOrDefault(m => m.Email == User.Identity.Name);
            var profile = db.GetAllProfiles().FirstOrDefault(m => m.UserId == user.UserId);
            var account = db.GetAllAccounts().FirstOrDefault(m => m.UserId == user.UserId);

            var transactions = db.GetAllTransactions().Where(m => m.AccountId == account.AccountId).ToList();

            int total = 0;

            foreach (var item in transactions)
            {
                total += item.Amount;

            }
            account.Balance = total;
            db.UpdateAccount(account);

            ProfileViewModel model = new ProfileViewModel();
            model.user = user;
            model.profile = profile;
            model.account = account;

            return View("ViewProfile", model);
        }

        // POST: AccountController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        [HttpGet]
        public IActionResult MakeClaim() {
            CreateClaimViewModel model = new CreateClaimViewModel();
            var selectedUser = db.GetAllUsers().FirstOrDefault(m => m.Email == User.Identity.Name);
            var policies = db.GetAllTransactions().Where(m => m.UserId == selectedUser.UserId).Select(m => m.PolicyId).Distinct().ToList();
            model.Policies = db.GetAllPolicys().Where(m => policies.Contains(m.PolicyId)).ToList();
            return View(model); }

        [HttpPost]
        public IActionResult MakeClaim(CreateClaimViewModel model) {
            var selecteduser = db.GetAllUsers().FirstOrDefault(m => m.Email == User.Identity.Name);
            var selectedprofile = db.GetAllProfiles().FirstOrDefault(m => m.UserId == selecteduser.UserId);
            var selectedaccount = db.GetAllAccounts().FirstOrDefault(m => m.UserId == selecteduser.UserId);
            var selectedpolicy = db.GetAllPolicys().FirstOrDefault(m => m.PolicyId == model.PolicyId);

            InsuranceDLL.DataAccess.DomainModels.Transaction transaction = new InsuranceDLL.DataAccess.DomainModels.Transaction();
            transaction.AccountId = selectedaccount.AccountId;
            transaction.Amount = model.Cost;
            transaction.Notes = "UnapprovedClaim";
            transaction.PolicyId = selectedpolicy.PolicyId;
            transaction.ProfileId = selectedprofile.ProfileId;
            transaction.TimeSent = DateTime.Now;
            transaction.TransactionId = Guid.NewGuid().ToString();
            transaction.UserId = selecteduser.UserId;

            Claim claim = new Claim();
            claim.Notes = "Claim";
            claim.PolicyId = selectedpolicy.PolicyId;
            claim.ProfileId = selectedprofile.ProfileId;
            claim.Type = "Unapproved";
            claim.UserId = selecteduser.UserId;
            claim.ClaimId = Guid.NewGuid().ToString();
            claim.Cost = model.Cost;
            claim.Description = model.Description;
            claim.AccountId = selectedaccount.AccountId;


            transaction.ClaimId = claim.ClaimId;

            db.AddTransaction(transaction);
            db.AddClaim(claim);
            return View(); }

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

        [HttpGet]
        public IActionResult GetAllClaimApplications()
        {
            var selectedUser = db.GetAllUsers().FirstOrDefault(m => m.Email == User.Identity.Name);

            var applications = db.GetAllTransactions().Where(m => m.Notes == "UnapprovedClaim").ToList();
            var claims = db.GetAllClaims().Where(m => m.Type == "Unapproved").ToList();

            var claimpolicyids = applications.Select(m => m.PolicyId).Distinct().ToList();
            var claimuserids = applications.Select(m => m.UserId).Distinct().ToList();

            var policiess = db.GetAllPolicys().Where(m => claimpolicyids.Contains(m.PolicyId)).ToList();
            var users = db.GetAllUsers().Where(m => claimuserids.Contains(m.UserId)).ToList();
            var profiles = db.GetAllProfiles().Where(m => claimuserids.Contains(m.UserId)).ToList();
            var accounts = db.GetAllAccounts().Where(m => claimuserids.Contains(m.UserId)).ToList();

       

            ViewAllClaimsViewModel model = new ViewAllClaimsViewModel();

            for (int i = 0; i < users.Count; i++)
            {
                var newcliam = new ClaimViewModel(applications[i], claims[i], users[i], profiles[i], accounts[i], policiess[i]);
                model.Claims.Add(newcliam);
                }

            return View(model);
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

