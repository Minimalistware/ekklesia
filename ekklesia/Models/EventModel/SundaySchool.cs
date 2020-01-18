using ekklesia.Models.MemberModel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ekklesia.Models.EventModel
{
    public class SundaySchool
    {
        [ForeignKey(nameof(Meeting))]
        public int MeetingId { get; set; }
        public Meeting Meeting { get; set; }

        [Required]
        public string Theme { get; set; }
        [Required]
        public string Verse { get; set; }
        [Required]
        public int NumberOfBibles { get; set; }

    }
}
