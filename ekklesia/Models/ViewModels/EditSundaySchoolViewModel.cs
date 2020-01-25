﻿using ekklesia.Models.EventModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ekklesia.Models.ViewModels
{
    public class EditSundaySchoolViewModel : CreateSundaySchoolViewModel
    {
        public EditSundaySchoolViewModel(SundaySchool occasion)
        {
            Id = occasion.Id;
            //Teacher = occasion.Speaker;
            Theme = occasion.Theme;
            Verse = occasion.Verse;

        }

        public int Id { get; set; }
        public string PageTitle { get; set; }
    }
}
