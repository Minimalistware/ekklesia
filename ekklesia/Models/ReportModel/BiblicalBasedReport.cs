using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ekklesia.Models.ReportModel
{
    public class BiblicalBasedReport : Report
    {
        public BiblicalBasedReport()
        {
            Type = ReportType.BIBLÍCO;
        }

        [Required]
        public int Bibles { get; set; }
        [Required]
        public int ReunionWithTeachers { get; set; }
        [Required]
        public int Visitants { get; set; }
        [Required]
        public int PeoplePresent { get; set; }
        [Required]
        public int PedagogicalBody { get; set; }

    }
}
