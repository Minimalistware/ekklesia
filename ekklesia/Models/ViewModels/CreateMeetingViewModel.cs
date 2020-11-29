using ekklesia.Models.MemberModel;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ekklesia.Models.ViewModels
{
    public abstract class CreateMeetingViewModel : CreateEventViewModel
    {
        protected CreateMeetingViewModel()
        {
            PresentMembers = new HashSet<Member>();
            SelectedMembers = new HashSet<int>();
            AllMembers = new HashSet<SelectListItem>();
            Date = DateTime.Now;
        }
        
        [Required]
        public int TeacherId { get; set; }
        [Required]
        public IEnumerable<int> SelectedMembers { get; set; }
                
        
        public HashSet<SelectListItem> AllMembers { get; set; }

        public IEnumerable<Member> PresentMembers { get; set; }

        public abstract void AddMember(SelectListItem item);
    }
}
