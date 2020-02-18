using System;
using System.ComponentModel.DataAnnotations;

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
