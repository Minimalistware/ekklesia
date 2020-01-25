using ekklesia.Models.MemberModel;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ekklesia.Models.ViewModels
{
    public class CreateReunionViewModel : CreateEventViewModel
    {

        public int SpeakerId { get; set; }
        public int[] MemberIds { get; set; }

        [Required]
        public string Topic { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

    }
}
