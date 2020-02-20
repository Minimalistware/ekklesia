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
            PresentMembers = new HashSet<Member>();
            SelectedMembers = new HashSet<string>();
            AllMembers = new HashSet<SelectListItem>();
        }
        
        [Required]
        public string TeacherId { get; set; }
        [Required]
        public IEnumerable<string> SelectedMembers { get; set; }
                
        
        public HashSet<SelectListItem> AllMembers { get; set; }

        public IEnumerable<Member> PresentMembers { get; set; }

        public abstract void AddMember(SelectListItem item);
    }
}
