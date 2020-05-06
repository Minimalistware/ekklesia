using ekklesia.Models.ReportModel;
using System.ComponentModel.DataAnnotations;

namespace ekklesia.Models.ViewModels
{
    public class ReportSearchViewModel
    {

        public ReportSearchViewModel()
        {
            PageTitle = "Buscar Relatório";
        }

        public ReportType? ReportType { get; set; }


        public int? Months { get; set; }

        public string PageTitle { get; set; }
    }
}
