using ekklesia.Models.MemberModel;
using System.Collections.Generic;

namespace ekklesia.Models.EventModel
{
    public abstract class Presentable : Event
    {
        public Presentable()
        {
            this.Members = new HashSet<Member>();

        }
        public ICollection<Member> Members { get; set; }
        public Member Speaker { get; set; }

    }
}
