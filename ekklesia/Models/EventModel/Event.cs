using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ekklesia.Models.EventModel
{
    public abstract class Event
    {

        public int Id { get; protected set; }
        [Required]
        public DateTime Date { get; set; }

    }
}
