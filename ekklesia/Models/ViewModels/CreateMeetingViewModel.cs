using ekklesia.Models.MemberModel;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ekklesia.Models.ViewModels
{
    public abstract class CreateMeetingViewModel : CreateEventViewModel
    {
        protected CreateMeetingViewModel()
        {
            PresentMembers = new List<Member>();
            SelectedMembers = new List<string>();
            AllMembers = new List<SelectListItem>();
        }
        
        [Required]
        public string TeacherId { get; set; }
        [Required]
        public IEnumerable<string> SelectedMembers { get; set; }
                
        public List<SelectListItem> AllMembers { get; set; }

        public IEnumerable<Member> PresentMembers { get; set; }

    }
}
