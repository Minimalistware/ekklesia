using ekklesia.Models.MemberModel;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ekklesia.Models.ViewModels
{
    public class BaptismCreateViewModel : CreateMeetingViewModel
    {
        public BaptismCreateViewModel()
        {
            AllMembers = new HashSet<SelectListItem>();
        }

        public BaptismCreateViewModel(HashSet<SelectListItem> members)
        {
            this.AllMembers = members;
        }
        [Required]
        public string Place { get; set; }
                

        public override void AddMember(SelectListItem item)
        {
            AllMembers.Add(item);
        }
    }
}
