using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ekklesia.Models.ViewModels
{
    public abstract class TransactionCreateViewModel
    {


        public HashSet<SelectListItem> AllEvents { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public decimal Value { get; set; }
        public int OccasionId { get; set; }
    }
}
