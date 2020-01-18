using ekklesia.Models.MemberModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ekklesia.Models.EventModel
{
    public class Reunion
    {
        
        [ForeignKey(nameof(Meeting))]
        public int MeetingId { get; set; } // PK and FK pointing to PersonTpt
        public Meeting Meeting { get; set; }


        [Required]
        public string Topic { get; set; }
        [Required]
        public DateTime EndTime { get; set; }

    }
}
