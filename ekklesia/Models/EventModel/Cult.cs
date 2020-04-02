using ekklesia.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ekklesia.Models.EventModel
{
    [Table("Cults")]
    public class Cult : Occasion
    {
        public Cult()
        {
            EventType = EventType.CULTO;
        }

        public Cult(CreateCultViewModel model)
        {
            Date = model.Date;
            EventType = EventType.CULTO;
            NumberOfPeople = model.NumberOfPeople;
            MainVerse = model.MainVerse;
        }

        
        [Required]
        public int NumberOfPeople { get; set; }
        [Required]
        public string MainVerse { get; set; }
        [Required]
        public CultType CultType { get; set; }
        [Required]
        public bool Internal { get; set; }

    }
}
