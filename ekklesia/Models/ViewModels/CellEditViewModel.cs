using ekklesia.Models.EventModel;

namespace ekklesia.Models.ViewModels
{
    public class CellEditViewModel : CellCreateViewModel
    {
        public CellEditViewModel()
        {
            PageTitle = "Editar Célula";
        }

        public CellEditViewModel(Cell cell)
        {
            PageTitle = "Editar Célula";
            Id = cell.Id;
            Date = cell.Date;
            Convertions = cell.Convertions;
        }

        public int Id { get; set; }
        public string PageTitle { get; set; }

    }
}
