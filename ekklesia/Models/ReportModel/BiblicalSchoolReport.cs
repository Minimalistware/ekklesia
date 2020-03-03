using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ekklesia.Models.ReportModel
{
    public class BiblicalSchoolReport:Report
    {
        
        [Required]
        public int TeacherMeating { get; set; }
        [Required]
        public int Teachers { get; set; }
        [Required]
        public int Bibles { get; set; }
    }
}
