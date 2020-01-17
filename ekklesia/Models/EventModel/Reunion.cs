using ekklesia.Models.MemberModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ekklesia.Models.EventModel
{
    public class Reunion : Presentable
    {
        [Required]
        public string Topic { get; set; }
        [Required]
        public DateTime EndTime { get; set; }

    }
}
