using ekklesia.Models.MemberModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ekklesia.Models.EventModel
{
    public class OccasionMember
    {
        public int MemberId { get; set; }
        public Member Member { get; set; }
        public int OccasionId { get; set; }
        public Occasion Occasion { get; set; }
    }
}
