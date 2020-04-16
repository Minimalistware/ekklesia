
using System.ComponentModel.DataAnnotations;

namespace ekklesia.Models.ViewModels
{
    public class YoungBasedReportViewModel : ReportCreateViewModel
    {
        public YoungBasedReportViewModel()
        {
            this.Type = ReportModel.ReportType.CÉLULA;            
        }

        [Required]
        public int MeetingsWithTheCoordination { get; set; }
        [Required]
        public int Visits { get; set; }
        [Required]
        public int BoardMembersNumber { get; set; }
        [Required]
        public int Evangelisms { get; set; }
    }
}
