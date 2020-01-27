using ekklesia.Models.MemberModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ekklesia.Models.EventModel
{
    public abstract class Meeting : Event
    {
        protected Meeting()
        {
            this.Members = new List<MeetingMember>();
        }

        public ICollection<MeetingMember> Members { get; set; }
        public Member Speaker { get; set; }

        public void AddMember(Member member)
        {
            Members.Add(new MeetingMember() { Member = member });
        }
    }
}
