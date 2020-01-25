using ekklesia.Models.MemberModel;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ekklesia.Models.ViewModels
{
    public class CreateSundaySchoolViewModel : CreateEventViewModel
    {

        public int TeacherId { get; set; }
        public int[] MemberIds { get; set; }

        [Required]
        public string Theme { get; set; }
        [Required]
        public string Verse { get; set; }
        [Required]
        public int NumberOfBibles { get; set; }

        
        

    }
}
