using ekklesia.Models.MemberModel;
using ekklesia.Models.ViewModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ekklesia.Models.EventModel
{

    [Table("SundaySchools")]
    public class SundaySchool : Occasion
    {

        [Required]
        public string Theme { get; set; }
        [Required]
        public string Verse { get; set; }
        [Required]
        public int NumberOfBibles { get; set; }
        [Required]
        public Member Teacher { get; set; }
        public ICollection<OccasionMember> Members { get; set; }

        public SundaySchool()
        {
            this.Members = new HashSet<OccasionMember>();
        }

        public SundaySchool(CreateSundaySchoolViewModel model)
        {
            Date = model.Date;
            EventType = EventType.Escola_Dominical;
            Theme = model.Theme;
            Verse = model.Verse;
            NumberOfBibles = model.NumberOfBibles;
            this.Members = new HashSet<OccasionMember>();
        }

        public void AddMember(Member member)
        {
            this.Members.Add(new OccasionMember() { Member = member, Occasion = this });
        }

    }
}
