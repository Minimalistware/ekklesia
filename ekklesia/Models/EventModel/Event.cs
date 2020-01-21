using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ekklesia.Models.EventModel
{
    public class Event
    {

        public int Id { get; set; }
        [Required]
        public DateTime Date { get; set; }
        public EventType EventType { get; set; }

    }
}
