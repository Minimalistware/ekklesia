using ekklesia.Models.EventModel;
using ekklesia.Models.MemberModel;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ekklesia.Models.ViewModels
{
    public class EditSundaySchoolViewModel : CreateSundaySchoolViewModel
    {
        public EditSundaySchoolViewModel(SundaySchool sunday, List<SelectListItem> allMembers)
            :base(allMembers)
        {
            Id = sunday.Id;
            //Teacher = occasion.Speaker;
            Theme = sunday.Theme;
            Verse = sunday.Verse;

        }

        public int Id { get; set; }
        public string PageTitle { get; set; }
    }
}
