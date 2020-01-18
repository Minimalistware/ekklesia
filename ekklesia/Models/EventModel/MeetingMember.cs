using ekklesia.Models.MemberModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ekklesia.Models.EventModel
{
    public class MeetingMember
    {
        public int MemberId { get; set; }
        public Member Member { get; set; }
        public int MeetingId { get; set; }
        public Meeting Meeting { get; set; }
    }
}
