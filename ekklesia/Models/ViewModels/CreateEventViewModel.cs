using System;
using System.ComponentModel.DataAnnotations;

namespace ekklesia.Models.ViewModels
{
    public abstract class CreateEventViewModel
    {
        [Required]
        public DateTime Date { get; set; }
    }
}

