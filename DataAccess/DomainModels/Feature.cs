using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace InsuranceDLL.DataAccess.DomainModels
{
    public class Feature
    {
        [Key]
        public int id { get; set; }
        public string FeatureId { get; set; }
        public string Code { get; set; }
        public DateTime DateValid { get; set; }
        public int Type { get; set; }
        public string Notes { get; set; }
    }
}
