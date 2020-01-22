using ekklesia.Models.EventModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ekklesia.Models.ViewModels
{
    public abstract class CreateEventViewModel
    {
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public EventType EventType { get; set; }
    }
}

