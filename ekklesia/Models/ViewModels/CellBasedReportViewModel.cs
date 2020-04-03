using System.ComponentModel.DataAnnotations;

namespace ekklesia.Models.ViewModels
{
    public class CellBasedReportViewModel : ReportCreateViewModel
    {

        public CellBasedReportViewModel()
        {
            this.Type = ReportModel.ReportType.CÉLULA;
        }

        [Required]
        public int CoordenationMeatings { get; set; }
        [Required]
        public int NumberOfVisits { get; set; }
        [Required]
        public int NumberOfEvangelisms { get; set; }
        [Required]
        public int NumberOfBoardMembers { get; set; }
    }
}
