using ekklesia.Models.ViewModels;
using System;

namespace ekklesia.Models.EventModel
{
    public class Cell:Occasion
    {
        public Cell()
        {
            EventType = EventType.CÉLULA;
            Date = DateTime.Now;
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
