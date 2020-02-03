using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ekklesia.Models.ViewModels
{
    public abstract class CreateMeetingViewModel : CreateEventViewModel
    {
        protected CreateMeetingViewModel()
        {

        }
        public CreateMeetingViewModel(List<SelectListItem> allMembers)
        {
            AllMembers = allMembers;
        }

        [Required]
        public string TeacherId { get; set; }
        [Required]
        public IEnumerable<string> SelectedMembers { get; set; }
                
        public List<SelectListItem> AllMembers { get; set; }
       
    }
}
