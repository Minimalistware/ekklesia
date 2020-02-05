using ekklesia.Models.MemberModel;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ekklesia.Models.ViewModels
{
    public class CreateReunionViewModel : CreateMeetingViewModel
    {
        public CreateReunionViewModel()
        {
        }

        public CreateReunionViewModel(List<SelectListItem> allMembers)
        {
            AllMembers = allMembers;
        }

        [Required]
        public string Topic { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

    }
}
