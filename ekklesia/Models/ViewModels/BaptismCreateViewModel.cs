using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ekklesia.Models.ViewModels
{
    public class BaptismCreateViewModel : CreateEventViewModel
    {
        public BaptismCreateViewModel()
        {
            AllMembers = new List<SelectListItem>();
        }

        public BaptismCreateViewModel(List<SelectListItem> members)
        {
            this.AllMembers = members;
        }
        [Required]
        public string Place { get; set; }
        public int BaptizerId { get; set; }
        [Required]
        public IEnumerable<string> BaptizerMembers { get; set; }

        public List<SelectListItem> AllMembers { get; set; }
    }
}
