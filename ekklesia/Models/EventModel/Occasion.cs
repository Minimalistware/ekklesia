using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ekklesia.Models.EventModel
{
    
    public abstract class Occasion
    {
        public int Id { get; set; }
        [Required]
        public DateTime Date { get; set; }
        public EventType EventType { get; set; }

    }
}
