using ekklesia.Models.MemberModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ekklesia.Models.EventModel
{
    public abstract class Meeting : Event
    {
        public ICollection<MeetingMember> Members { get; set; }
        public Member Speaker { get; set; }

    }
}
