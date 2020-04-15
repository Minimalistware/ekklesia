using ekklesia.Models.ReportModel;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ekklesia.Models.ViewModels
{
    public class GroupBasedReportViewModel : ReportCreateViewModel
    {
        public GroupBasedReportViewModel() { }

        public GroupBasedReportViewModel(ReportType Type)
        {
            this.Type = Type;
            this.AllMembers = new HashSet<SelectListItem>();
        }

        [Required]
        public int NumberOfVisits { get; set; }
        [Required]
        public int ExternalCults { get; set; }
        [Required]
        public int CellsNumber { get; set; }
        [Required]
        public int Baptized { get; set; }
        [Required]
        public int MeetingsWithTheCoordination { get; set; }
        [Required]
        public int MembersNumber { get; set; }
        [Required]
        public int BoardMembersNumber { get; set; }


    }
}
