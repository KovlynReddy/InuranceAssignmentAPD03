using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace InsuranceDLL.DataAccess.DomainModels
{
    public class User
    {
        [Key]
        public int id { get; set; }
        public int FamilyMembers { get; set; }
        public string UserId { get; set; }
        public string Email { get; set; }
        public string AccountId { get; set; }
        public string ProfileId { get; set; }
        public string FamilyId { get; set; }
        public DateTime TimeCreated { get; set; }
    }
}
