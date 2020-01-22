using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ekklesia.Models.ViewModels
{
    public class CreateCultViewModel: CreateEventViewModel
    {
        [Required]
        public int NumberOfPeople { get; set; }
        [Required]
        public string MainVerse { get; set; }
    }
}
