using ekklesia.Models.ViewModels;
using System.Threading.Tasks;

namespace ekklesia.Models
{
    public interface IFilling
    {
        Task<ReportCreateViewModel> FillOutBaseReport(ReportCreateViewModel model);
        IFilling Next { get; set; }
    }

}
