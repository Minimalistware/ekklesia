using ekklesia.Models.EventModel;
namespace ekklesia.Models.ViewModels
{
    public class EditCultViewModel : CreateCultViewModel
    {

        public EditCultViewModel(Cult cult)
        {
            Id = cult.Id;
            Date = cult.Date;
            NumberOfPeople = cult.NumberOfPeople;
            MainVerse = cult.MainVerse;
            PageTitle = "Editar Culto";
        }

        public int Id { get; set; }
        public string PageTitle { get; set; }
    }
}
