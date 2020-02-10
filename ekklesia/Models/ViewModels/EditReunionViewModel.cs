using ekklesia.Models.EventModel;
namespace ekklesia.Models.ViewModels
{
    public class EditReunionViewModel : CreateReunionViewModel
    {
        public EditReunionViewModel() { }

        public EditReunionViewModel(Reunion reunion, string pageTitle = "Editar Reunião")
        {
            Id = reunion.Id;
            Date = reunion.Date;
            EndTime = reunion.EndTime;
            TeacherId = reunion.SpeakerId.ToString();
            Topic = reunion.Topic;
            PageTitle = pageTitle;
        }

        public int Id { get; set; }
        public string PageTitle { get; set; }

    }
}
