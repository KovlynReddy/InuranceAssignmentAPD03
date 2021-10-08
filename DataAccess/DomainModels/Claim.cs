using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace InsuranceDLL.DataAccess.DomainModels
{
    public class Claim
    {
        [Key]
        public int id { get; set; }
        public string ClaimId { get; set; }
        public string AccountId { get; set; }
        public string UserId { get; set; }
        public string ProfileId { get; set; }
        public string PolicyId { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public string Notes { get; set; }
        public int Cost { get; set; }

    }
}
