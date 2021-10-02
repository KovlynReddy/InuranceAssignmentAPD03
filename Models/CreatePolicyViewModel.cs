using InsuranceDLL.DataAccess.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InuranceAssignmentAPD03.Models
{
    public class CreatePolicyViewModel : Policy
    {
        public List<string> TypeOptions { get; set; } = new List<string> {"Funeral Policy", "Death Insuance" };
    }
}
