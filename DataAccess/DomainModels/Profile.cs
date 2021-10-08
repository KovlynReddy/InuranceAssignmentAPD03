using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace InsuranceDLL.DataAccess.DomainModels
{
    public class Profile
    {
        [Key]
        public int id { get; set; }
        public string ProfileId { get; set; } //cover applied to 
        public string UserId { get; set; } // account creator
        public int FamilyMembers { get; set; } // need
        public string FamilyId { get; set; }
        public string Gender { get; set; } // need 
        public string UserName { get; set; } // need
        public int Age { get; set; } // need
        public DateTime DOB { get; set; } // need

        #region Details
        public int Sinus { get; set; }
        public int HeridataryDeseases { get; set; }
        public int Diabeties { get; set; }
        public int Cancer { get; set; }
        public int TerminalIllnesses { get; set; }
        public int Kids { get; set; }
        public int DangerousWorkingEnviroment { get; set; }
        public int TravelForWork { get; set; }
        public int SmokeCigerrets { get; set; }
        public int DrinkAlcohol { get; set; }
        public int OnPercesciptiveDrugs { get; set; }
        public int OnCronicDrugs { get; set; } 
        #endregion
    }
}
