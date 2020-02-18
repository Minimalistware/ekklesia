using ekklesia.Models.MemberModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ekklesia.Models.EventModel
{
    public class Baptism: Occasion
    {
        [Required]
        public string Place { get; set; }
        public int BaptizerId { get; set; }
        public Member Baptizer { get; set; }
        public ICollection<OccasionMember> Baptizeds { get; set; }
    }
}
