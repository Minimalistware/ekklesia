using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ekklesia.Models.Report
{
    public class Report
    {
        [Required]
        public DateTime Date { get; set; }

    }
}
