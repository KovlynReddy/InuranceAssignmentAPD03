using InsuranceDLL.DataAccess.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InuranceAssignmentAPD03.Models
{
    public class CreateClaimViewModel : Claim
    {
        public List<string> Reasons { get; set; } =new List<string>{ " "," "};
        public List<Policy> Policies { get; set; } 
    }
}
