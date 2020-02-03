using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ekklesia.Models.ViewModels
{
    public class CreateSundaySchoolViewModel : CreateMeetingViewModel
    {
        public CreateSundaySchoolViewModel() { }

        public CreateSundaySchoolViewModel(List<SelectListItem> allMembers) : base(allMembers)
        {
        }

        [Required]
        public string Theme { get; set; }
        [Required]
        public string Verse { get; set; }
        [Required]
        public int NumberOfBibles { get; set; }

    }
}
