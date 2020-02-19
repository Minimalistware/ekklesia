using ekklesia.Models.MemberModel;
using ekklesia.Models.ViewModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ekklesia.Models.EventModel
{
    public class Baptism : Occasion
    {
        public Baptism()
        {
            Baptizeds = new HashSet<OccasionMember>();
        }

        public Baptism(BaptismCreateViewModel model)
        {
            Date = model.Date;
            Place = model.Place;
            BaptizerId = model.BaptizerId;
            EventType = EventType.BATISMO;
            Baptizeds = new HashSet<OccasionMember>();
        }

        [Required]
        public string Place { get; set; }
        public int BaptizerId { get; set; }
        public Member Baptizer { get; set; }
        public ICollection<OccasionMember> Baptizeds { get; set; }

        public void AddMember(Member member)
        {
            this.Baptizeds.Add(new OccasionMember() { Member = member, Occasion = this });
        }
    }
}
