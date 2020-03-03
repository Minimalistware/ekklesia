using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ekklesia.Models.ReportModel
{
    public class WomenGroupReport:Report
    {
        
        [Required]
        public int ExternalCults { get; set; }
        [Required]
        public int CoordenationMeatings { get; set; }

    }
}
