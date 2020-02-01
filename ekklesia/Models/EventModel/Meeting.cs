using ekklesia.Models.MemberModel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ekklesia.Models.EventModel
{
    public abstract class Meeting : Occasion
    {
        protected Meeting()
        {
            this.Members = new List<OccasionMember>();
        }

        public ICollection<OccasionMember> Members { get; set; }
        [Required]
        public Member Speaker { get; set; }
        
        public void AddMember(Member member)
        {
            this.Members.Add(new OccasionMember() { Member = member });
        }
    }
}
