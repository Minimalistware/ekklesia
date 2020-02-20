using ekklesia.Models.MemberModel;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ekklesia.Models.ViewModels
{
    public class BaptismCreateViewModel : CreateEventViewModel
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
        public int BaptizerId { get; set; }
        [Required]
        public IEnumerable<string> BaptizedMembersIds { get; set; }

        public HashSet<SelectListItem> AllMembers { get; set; }

        public void AddMember(SelectListItem item)
        {
            AllMembers.Add(item);
        }

                
    }
}
