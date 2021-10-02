using InsuranceDLL.DataAccess.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace InuranceAssignmentAPD03.Models
{
    public class ViewAllApplicantionsViewModel
    {
        public List<InsuranceDLL.DataAccess.DomainModels.Transaction> Applications { get; set; } = new List<InsuranceDLL.DataAccess.DomainModels.Transaction>();
        public List<User> Applicants { get; set; } = new List<User>();
        public List<Policy> SelectedPolicies { get; set; } = new List<Policy>();
        public List<ApplicationViewModel> ApplicationModels { get; set; } = new List<ApplicationViewModel>();
    }
}
