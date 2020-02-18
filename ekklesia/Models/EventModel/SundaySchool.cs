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
        public Member Teacher { get; set; }
        public int TeacherId { get; set; }
        public ICollection<OccasionMember> Members { get; set; }

        public SundaySchool()
        {
            this.Members = new HashSet<OccasionMember>();
        }

        public SundaySchool(CreateSundaySchoolViewModel model)
        {
            Date = model.Date;
            EventType = EventType.ESCOLA_DOMINICAL;
            Theme = model.Theme;
            Verse = model.Verse;
            NumberOfBibles = model.NumberOfBibles;
            this.Members = new HashSet<OccasionMember>();
        }

        public void AddMember(Member member)
        {
            this.Members.Add(new OccasionMember() { Member = member, Occasion = this });
        }

        public bool Contains(Member member)
        {
            var om = new OccasionMember() { Member = member, Occasion = this };
            return this.Members.Contains(om);
        }

        public bool Remove(Member member)
        {
            var om = new OccasionMember() { Member = member, Occasion = this };
            return this.Members.Remove(om);
        }
    }
}
