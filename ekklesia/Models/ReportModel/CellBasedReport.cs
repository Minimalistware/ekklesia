using System.ComponentModel.DataAnnotations;

namespace ekklesia.Models.ReportModel
{
    public class CellBasedReport : Report
    {
        public CellBasedReport()
        {
            ReportType = ReportType.CÉLULA;
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
