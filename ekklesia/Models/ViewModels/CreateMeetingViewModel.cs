using ekklesia.Models.MemberModel;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ekklesia.Models.ViewModels
{
    public abstract class CreateMeetingViewModel : CreateEventViewModel
    {
        protected CreateMeetingViewModel()
        {

        }
        public CreateMeetingViewModel(List<SelectListItem> members)
        {
            Members = members;
        }

        [Required]
        public string TeacherId { get; set; }
        [Required]
        public IEnumerable<string> SelectedMembers { get; set; }
                
        public List<SelectListItem> Members { get; set; }
        //public List<Member> MembersList { get; set; }

    }
}
