using InsuranceDLL.DataAccess.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InuranceAssignmentAPD03.Models
{
    public class ViewAllClaimsViewModel
    {
        public List<ClaimViewModel> Claims { get; set; } = new List<ClaimViewModel>();
    }
}
