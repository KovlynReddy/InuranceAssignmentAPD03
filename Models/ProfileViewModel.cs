using InsuranceDLL.DataAccess.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InuranceAssignmentAPD03.Models
{
    public class ProfileViewModel
    {
        public Account account { get; set; }
        public User user { get; set; }
        public Profile profile { get; set; }
        public int depositAmount { get; set; }

    }
}
