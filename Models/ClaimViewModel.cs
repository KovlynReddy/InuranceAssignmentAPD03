using InsuranceDLL.DataAccess.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace InuranceAssignmentAPD03.Models
{
    public class ClaimViewModel
    {
        public InsuranceDLL.DataAccess.DomainModels.Transaction application { get; set; } = new InsuranceDLL.DataAccess.DomainModels.Transaction();
        public Claim claim { get; set; }
        public User user { get; set; }
        public Profile profile { get; set; }
        public Account account { get; set; }
        public Policy policy { get; set; }

        public ClaimViewModel(InsuranceDLL.DataAccess.DomainModels.Transaction tra , Claim cla , User us , Profile pr , Account acc , Policy pol )
        {
            this.account = acc;
            this.application = tra;
            this.claim = cla;
            this.policy = pol;
            this.profile = pr;
            this.user = us;
        }
    }
}
