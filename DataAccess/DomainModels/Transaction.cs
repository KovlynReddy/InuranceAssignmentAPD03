using System;
using System.Collections.Generic;
using System.Text;

namespace InsuranceDLL.DataAccess.DomainModels
{
    public class Transaction
    {
        public int id { get; set; }
        public string TransactionId { get; set; }
        public string UserId { get; set; }
        public string ProfileId { get; set; }
        public string AccountId { get; set; }
        public string ClaimId { get; set; }
        public string PolicyId { get; set; }
        public string Notes { get; set; }
        public int Amount { get; set; }
        public DateTime TimeSent { get; set; }
    }
}
