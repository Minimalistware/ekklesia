using ekklesia.Models.MemberModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ekklesia.Models.ViewModels
{
    public class CreateSundaySchoolViewModel : CreateEventViewModel
    {
        [Required]
        public Member Teacher { get; set; }

        [Required]
        public string Theme { get; set; }
        [Required]
        public string Verse { get; set; }
        [Required]
        public int NumberOfBibles { get; set; }

        public ICollection<Member> Members { get; set; }


    }
}
