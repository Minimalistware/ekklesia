using ekklesia.Models.ViewModels;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ekklesia.Models.EventModel
{
    [Table("Cults")]
    public class Cult : Occasion
    {
        public Cult()
        {
            EventType = EventType.CULTO;
            Date = DateTime.Now;
        }

        public Cult(CreateCultViewModel model)
        {
            Date = model.Date;
            EventType = EventType.CULTO;
            NumberOfPeople = model.NumberOfPeople;
            MainVerse = model.MainVerse;
            Convertions = model.Convertions;
            Internal = model.InDoors;
        }


        [Required]
        public int NumberOfPeople { get; set; }
        [Required]
        public string MainVerse { get; set; }
        [Required]
        public CultType CultType { get; set; }
        [Required]
        public bool Internal { get; set; }
        [Required]
        public int Convertions { get; set; }

    }
}
