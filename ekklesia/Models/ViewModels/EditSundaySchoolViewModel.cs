using ekklesia.Models.EventModel;

namespace ekklesia.Models.ViewModels
{
    public class EditSundaySchoolViewModel : CreateSundaySchoolViewModel
    {
        public EditSundaySchoolViewModel() { }

        public EditSundaySchoolViewModel(SundaySchool school,
            string pageTitle = "Editar Escola Dominical")
        {
            Id = school.Id;
            Date = school.Date;
            TeacherId = school.TeacherId;
            Theme = school.Theme;
            Verse = school.Verse;
            NumberOfBibles = school.NumberOfBibles;
            PageTitle = pageTitle;
        }

        public int Id { get; set; }
        public string PageTitle { get; set; }
    }
}
