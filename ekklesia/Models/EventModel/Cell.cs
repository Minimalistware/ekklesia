using ekklesia.Models.ViewModels;

namespace ekklesia.Models.EventModel
{
    public class Cell:Occasion
    {
        public Cell()
        {
            EventType = EventType.CÉLULA;
        }

        public Cell(CellCreateViewModel model)
        {
            Date = model.Date;
            Convertions = model.Convertions;
            EventType = EventType.CÉLULA;
        }

        public int Convertions { get; set; }
    }
}
