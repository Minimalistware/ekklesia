using ekklesia.Migrations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ekklesia.Models.ViewModels
{
    public class CreateReunionViewModel: CreateEventViewModel
    {

        public Member Speaker { get; set; }

        [Required]
        public string Topic { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        public ICollection<Member> Members { get; set; }
    }
}
