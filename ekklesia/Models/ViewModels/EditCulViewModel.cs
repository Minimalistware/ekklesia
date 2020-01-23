using ekklesia.Models.EventModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ekklesia.Models.ViewModels
{
    public class EditCulViewModel : CreateCultViewModel
    {
        
        public EditCulViewModel(Cult cult)
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
