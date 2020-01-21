using ekklesia.Models.MemberModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ekklesia.Models.EventModel
{
    [Table("Reunions")]
    public class Reunion
    {
        //[ForeignKey(nameof(Event))]
        //public int Id { get; set; } // PK and FK pointing to Event
        //public Event Event { get; set; }



        public int MemberId { get; set; }
        public Member Speaker { get; set; }

        [Required]
        public string Topic { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        public ICollection<EventMember> Members { get; set; }

        public Reunion()
        {
            this.Members = new List<EventMember>();

        }


    }
}
