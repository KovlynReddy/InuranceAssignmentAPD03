using InsuranceDLL.DataAccess.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InuranceAssignmentAPD03.Models
{
    public class ApplicationViewModel
    {
        public InsuranceDLL.DataAccess.DomainModels.Transaction Applications { get; set; } 
        public User Applicants { get; set; } 
        public Policy SelectedPolicies { get; set; }

        public ApplicationViewModel(Policy pol , Transaction tran , User user)
        {
            this.Applicants       = user;
            this.Applications     = tran;
            this.SelectedPolicies = pol;
        }
    }
}
