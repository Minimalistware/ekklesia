using ekklesia.Models.MemberModel;
using ekklesia.Models.ViewModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ekklesia.Models.EventModel
{

    [Table("SundaySchools")]
    public class SundaySchool : Meeting
    {

        [Required]
        public string Theme { get; set; }
        [Required]
        public string Verse { get; set; }
        [Required]
        public int NumberOfBibles { get; set; }


        public SundaySchool()
        {
        }

        public SundaySchool(CreateSundaySchoolViewModel model)
        {
            Date = model.Date;
            EventType = EventType.Escola_Dominical;
           // Speaker = model.Teacher;
            Theme = model.Theme;
            Verse = model.Verse;
            NumberOfBibles = model.NumberOfBibles;
        }

    }
}
