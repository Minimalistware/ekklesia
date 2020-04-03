using ekklesia.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ekklesia.Models
{
    public interface IFilling
    {
        Task<ReportCreateViewModel> FillUpGroupReportModel(GroupBasedReportViewModel model);
        IFilling Next { get; set; }
    }
}
