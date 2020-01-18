using ekklesia.Models.MemberModel;
using System.Collections.Generic;

namespace ekklesia.Models.EventModel
{
    public class Meeting : Event
    {
        public int MeetingId { get; set; }
        public ICollection<MeetingMember> Members { get; set; }
        public Member Speaker { get; protected set; }

        public Meeting()
        {
            this.Members = new List<MeetingMember>();

        }

    }
}
