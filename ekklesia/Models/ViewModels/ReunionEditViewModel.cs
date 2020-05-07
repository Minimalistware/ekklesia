using ekklesia.Models.EventModel;
namespace ekklesia.Models.ViewModels
{
    public class ReunionEditViewModel : CreateReunionViewModel
    {
        public ReunionEditViewModel() { }

        public ReunionEditViewModel(Reunion reunion, string pageTitle = "Editar Reunião")
        {
            Id = reunion.Id;
            Date = reunion.Date;
            EndTime = reunion.EndTime;
            TeacherId = reunion.SpeakerId;
            Topic = reunion.Topic;
            PageTitle = pageTitle;
        }

        public int Id { get; set; }
        public string PageTitle { get; set; }

    }
}
