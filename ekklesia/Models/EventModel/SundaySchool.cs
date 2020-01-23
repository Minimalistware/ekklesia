using ekklesia.Models.MemberModel;
using ekklesia.Models.ViewModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ekklesia.Models.EventModel
{

    [Table("SundaySchools")]
    public class SundaySchool:Event
    {
        //[ForeignKey(nameof(Event))]
        //public int Id { get; set; } // PK and FK pointing to Event
        //public Event Event { get; set; }


        public int MemberId { get; set; }
        public Member Teacher { get; set; }

        [Required]
        public string Theme { get; set; }
        [Required]
        public string Verse { get; set; }
        [Required]
        public int NumberOfBibles { get; set; }

        public ICollection<EventMember> Members { get; set; }

        public SundaySchool()
        {
            this.Members = new List<EventMember>();

        }

        public SundaySchool(CreateSundaySchoolViewModel model)
        {
            Date = model.Date;
            EventType = EventType.Escola_Dominical;
            Teacher = model.Teacher;
            Theme = model.Theme;
            Verse = model.Verse;
            NumberOfBibles = model.NumberOfBibles;
        }
    }
}
