using ekklesia.Models.EventModel;

namespace ekklesia.Models.ViewModels
{
    public class BaptismEditViewModel : BaptismCreateViewModel
    {
        public BaptismEditViewModel()
        {
        }

        public BaptismEditViewModel(Baptism baptism)
        {
            PageTitle = "Editar Batismo";
            Id = baptism.Id;
            Date = baptism.Date;
            Place = baptism.Place;
            TeacherId = baptism.BaptizerId;

        }

        public int Id { get; set; }
        public string PageTitle { get; set; }
    }
}
