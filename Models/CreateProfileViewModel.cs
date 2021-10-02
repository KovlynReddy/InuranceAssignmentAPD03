using InsuranceDLL.DataAccess.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InuranceAssignmentAPD03.Models
{
    public class CreateProfileViewModel : Profile
    {
        public string ProfilePath { get; set; }

        public bool ASinus { get; set; }
        public bool AHeridataryDeseases { get; set; }
        public bool ADiabeties { get; set; }
        public bool ACancer { get; set; }
        public bool ATerminalIllnesses { get; set; }
        public bool AKids { get; set; }
        public bool ADangerousWorkingEnviroment { get; set; }
        public bool ATravelForWork { get; set; }
        public bool ASmokeCigerrets { get; set; }
        public bool ADrinkAlcohol { get; set; }
        public bool AOnPercesciptiveDrugs { get; set; }
        public bool AOnCronicDrugs { get; set; }
    }
}
