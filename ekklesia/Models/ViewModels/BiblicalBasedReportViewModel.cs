
using System.ComponentModel.DataAnnotations;

namespace ekklesia.Models.ViewModels
{
    public class BiblicalBasedReportViewModel : ReportCreateViewModel
    {
        public BiblicalBasedReportViewModel()
        {
            this.Type = ReportModel.ReportType.CÉLULA;
        }

        [Required]
        public int Bibles { get; set; }
        [Required]
        public int ReunionWithTeachers { get; set; }
        [Required]
        public int Visitants { get; set; }
        [Required]
        public int PeoplePresent { get; set; }
        [Required]
        public int PedagogicalBody { get; set; }
    }
}
