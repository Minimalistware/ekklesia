using System.ComponentModel.DataAnnotations;

namespace ekklesia.Models.ReportModel
{
    public class GroupBasedReport : Report
    {
        [Required]
        public int ExternalCults { get; set; }
        [Required]
        public int CellsNumber { get; set; }
        [Required]
        public int Baptized { get; set; }
        [Required]
        public int MeetingsWithTheCoordination { get; set; }

    }
}
