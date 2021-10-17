using InsuranceDLL.DataAccess.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InuranceAssignmentAPD03.Models
{
    public class ViewPolicyViewModel 
    {
        public Policy policy { get; set; }
        public List<Profile> profiles { get; set; } = new List<Profile>();
        public Profile SelectedProfile { get; set; }
        public string ProfileId { get; set; }
    }
}
