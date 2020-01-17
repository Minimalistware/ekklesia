using ekklesia.Models.MemberModel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ekklesia.Models.EventModel
{
    public class SundaySchool : Presentable
    {

        [Required]
        public string Theme { get; set; }
        [Required]
        public string Verse { get; set; }
        [Required]
        public int NumberOfBibles { get; set; }

    }
}
