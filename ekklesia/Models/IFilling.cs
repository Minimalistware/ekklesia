using ekklesia.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ekklesia.Models
{
    public interface IFilling
    {
        Task<ReportCreateViewModel> FillUpModel(ReportCreateViewModel model);
        IFilling Next { get; set; }
    }
}
