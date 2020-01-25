using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ekklesia.Models.ViewModels
{
    public class CompoundCreateEventViewModel
    {

       
        public CreateCultViewModel CreateCultView { get; set; }
        public CreateReunionViewModel CreateReunionView { get; set; }
        public CreateSundaySchoolViewModel CreateSundaySchoolView { get; set; }
    }
}
