using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace InsuranceDLL.DataAccess.DomainModels
{
    public class Account
    {
        [Key]
        public int id { get; set; }
        public string AccountId { get; set; }
        public string UserId { get; set; }
        public int Balance { get; set; }
    }
}
