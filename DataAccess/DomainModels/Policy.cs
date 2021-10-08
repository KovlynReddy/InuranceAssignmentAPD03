using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace InsuranceDLL.DataAccess.DomainModels
{
    public class Policy
    {
        [Key]
        public int id { get; set; }
        public string PolicyId { get; set; }
        public string PolicyName { get; set; }
        public string PolicyDescription { get; set; }
        public int Type { get; set; }
        public int BaseCost { get; set; } // base
        public int MonthlyPremium { get; set; } // after calulations    
        public int VAT { get; set; } // in cents
        public int AverageAge { get; set; } // a penalty if over
        public int Constant { get; set; } //  a constant
        public int Valid { get; set; } // is active
    }
}
