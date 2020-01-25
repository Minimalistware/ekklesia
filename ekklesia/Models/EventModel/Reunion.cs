using ekklesia.Models.MemberModel;
using ekklesia.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ekklesia.Models.EventModel
{
    [Table("Reunions")]
    public class Reunion : Meeting
    {

        [Required]
        public string Topic { get; set; }

        [Required]
        public DateTime EndTime { get; set; }


        public Reunion()
        {
        }

        public Reunion(CreateReunionViewModel model)
        {
            Date = model.Date;
            EventType = EventType.Reunião;
           // Speaker = model.Speaker;
            Topic = model.Topic;
            EndTime = model.EndTime;

        }


    }
}
