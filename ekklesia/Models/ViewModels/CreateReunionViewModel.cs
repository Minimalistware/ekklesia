using ekklesia.Models.EventModel;
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

        public CreateReunionViewModel(HashSet<SelectListItem> allMembers)
        {
            AllMembers = allMembers;
        }

        [Required]
        public string Topic { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        [Required]
        public ReunionType Type { get; set; }

        [Required]
        public ReunionType ReunionType { get; set; }

        public override void AddMember(SelectListItem item)
        {
            AllMembers.Add(item);
        }

       
    }
}
