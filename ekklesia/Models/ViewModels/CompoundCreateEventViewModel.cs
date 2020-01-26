using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ekklesia.Models.ViewModels
{
    public class CompoundCreateEventViewModel
    {
        public CompoundCreateEventViewModel() { }

        public CompoundCreateEventViewModel(List<SelectListItem> members)
        {
            CreateReunionView = new CreateReunionViewModel(members);
            CreateSundaySchoolView = new CreateSundaySchoolViewModel(members);
        }

        public CreateCultViewModel CreateCultView { get; set; }
        public CreateReunionViewModel CreateReunionView { get; set; }
        public CreateSundaySchoolViewModel CreateSundaySchoolView { get; set; }
    }
}
