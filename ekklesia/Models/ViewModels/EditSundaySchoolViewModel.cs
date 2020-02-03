using ekklesia.Models.EventModel;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace ekklesia.Models.ViewModels
{
    public class EditSundaySchoolViewModel : CreateSundaySchoolViewModel
    {
        public EditSundaySchoolViewModel() { }

        public EditSundaySchoolViewModel(SundaySchool school, List<SelectListItem> allMembers)
            : base(allMembers)
        {
            Date = school.Date;
            Id = school.Id;
            TeacherId = school.Teacher.Id.ToString();
            Theme = school.Theme;
            Verse = school.Verse;
            NumberOfBibles = school.NumberOfBibles;

        }

        public int Id { get; set; }
        public string PageTitle { get; set; }
    }
}
