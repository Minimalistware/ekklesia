using ekklesia.Models.MemberModel;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ekklesia.Models.ViewModels
{
    public abstract class CreateMeetingViewModel : CreateEventViewModel
    {
        public string TeacherId { get; set; }
        public IEnumerable<SelectListItem> Members { get; set; }
        public IEnumerable<string> SelectedMembers { get; set; }

        
    }
}
