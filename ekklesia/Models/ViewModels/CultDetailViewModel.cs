using ekklesia.Models.EventModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ekklesia.Models.ViewModels
{
    public class CultDetailViewModel
    {
        public CultDetailViewModel(Cult occasion, string pageTitle = "Detalhes do culto")
        {
            Cult = occasion;
            PageTitle = pageTitle;
        }

        public Cult Cult { get; set; }
        public string PageTitle { get; set; }
    }
}
