using ekklesia.Models.EventModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ekklesia.Models.ViewModels
{
    public class EventSearchViewModel
    {
        public EventSearchViewModel()
        {
            PageTitle = "Buscar Evento";
        }

        public EventType? EventType { get; set; }


        public int? Days { get; set; }

        public string PageTitle { get; set; }
    }
}
