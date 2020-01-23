using ekklesia.Models.MemberModel;
using ekklesia.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ekklesia.Models.EventModel
{
    [Table("Reunions")]
    public class Reunion : Event
    {

        public Member Speaker { get; set; }

        [Required]
        public string Topic { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        public ICollection<EventMember> Members { get; set; }

        public Reunion(CreateReunionViewModel model)
        {
            Date = model.Date;
            EventType = EventType.Reunião;
            Speaker = model.Speaker;
            Topic = model.Topic;
            EndTime = model.EndTime;

        }
    }
}
