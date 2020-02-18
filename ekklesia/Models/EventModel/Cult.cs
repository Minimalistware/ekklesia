﻿using ekklesia.Models.ViewModels;
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
        }

        public Cult(CreateCultViewModel model)
        {
            Date = model.Date;
            EventType = EventType.CULTO;
            NumberOfPeople = model.NumberOfPeople;
            MainVerse = model.MainVerse;
        }

        //[ForeignKey(nameof(Event))]
        //public int Id { get; set; } // PK and FK pointing to Event
        //public Event Event { get; set; }

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
