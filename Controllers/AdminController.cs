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
    public class AdminController : Controller
    {
        private readonly IInsuranceDB db;

        public AdminController(IInsuranceDB db)
        {
            this.db = db;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ViewPolicies() {

            var policies = db.GetAllPolicys();

            return View(policies); 
        }

        [HttpGet]
        public IActionResult EditPolicy(string id) {

            var policy = db.GetAllPolicys().FirstOrDefault(m=>m.PolicyId==id);


            return View("CreatePolicy",policy); 
        }

        [HttpPost]
        public IActionResult EditPolicy(Policy model) {

            db.UpdatePolicy(model);

            return View(); }


        [HttpGet]
        public IActionResult AdminConsole()
        {
            return View();
        }

        public async Task<IActionResult> SendOutDebitOrders() 
        {
            var applications = db.GetAllTransactions().Where(m => m.Notes == "Approved").ToList();

            var applicants = db.GetAllUsers();

            applicants = applicants.Where(m => applications.Select(g => g.UserId).ToList().Contains(m.UserId)).ToList();
            var selectedpolicies = db.GetAllPolicys().Where(m => applications.Select(g => g.PolicyId).ToList().Contains(m.PolicyId)).ToList();
            var selectedaccounts = db.GetAllAccounts().Where(m => applications.Select(g => g.UserId).ToList().Contains(m.UserId)).ToList();

            ViewAllApplicantionsViewModel model = new ViewAllApplicantionsViewModel();
            model.Applications = applications;
            model.Applicants = applicants;
            model.SelectedPolicies = selectedpolicies;

            for (int i = 0; i < model.Applications.Count; i++)
            {
                if ( model.Applications[i] != null && model.Applicants[i] != null)
                {

                    model.ApplicationModels.Add(new ApplicationViewModel(model.SelectedPolicies.FirstOrDefault(m => m.PolicyId == applications.FirstOrDefault(k=>k.UserId == model.Applicants[i].UserId).PolicyId), model.Applications[i], model.Applicants[i]));

                }

            }

            foreach (var user in applications)
            {
                // send out email and send out transaction
                InsuranceDLL.DataAccess.DomainModels.Transaction deposit = new InsuranceDLL.DataAccess.DomainModels.Transaction();
                deposit.AccountId = user.AccountId;
                deposit.Amount = user.Amount;
                deposit.ClaimId = user.ClaimId; // change
                deposit.Notes = "DebitOrder";
                deposit.PolicyId = user.PolicyId; // change
                deposit.ProfileId = " ";
                deposit.TimeSent = DateTime.Now;
                deposit.UserId = user.UserId;
                deposit.TransactionId = Guid.NewGuid().ToString();

                db.AddTransaction(deposit);

                await SendMail($"Thank you for your support of our bussiness in subscribing to the for the amount of R {user.Amount} and your balance is : {selectedaccounts.FirstOrDefault(m=>m.UserId == user.UserId).Balance}",applicants.FirstOrDefault(m=>m.UserId==user.UserId).Email);
                // send email

            }

            return View();
        }
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
        public async Task SendMail(string amessage , string address)
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
                .Subject(subject: " Policy Payment " )
                .Body(body: "Policy Payment \n" + amessage)
                .SendAsync();

            int result = 0;
            #endregion
        }

    }
}
