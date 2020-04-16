using ekklesia.Models.ReportModel;
using ekklesia.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ekklesia.Controlers
{
    public class ReportController : Controller
    {
        private readonly IReportRepository repository;
        private readonly IReportBuilder reportBuilder;

        public ReportController(IReportRepository repository, IReportBuilder builder)
        {
            this.repository = repository;
            this.reportBuilder = builder;
        }

        public IActionResult List()
        {
            var reports = repository.GetReports();
            return View(reports);
        }

        [HttpGet]
        public ViewResult Create()
        {
            var childrenViewModel = new GroupBasedReportViewModel(ReportType.CRIANÇAS);
            var menViewModel = new GroupBasedReportViewModel(ReportType.HOMENS);
            var womenViewModel = new GroupBasedReportViewModel(ReportType.MULHERES);
            var youngViewModel = new YoungBasedReportViewModel();
            var biblicalViewModel = new BiblicalBasedReportViewModel();


            reportBuilder.FilloutGroupReport(childrenViewModel);

            ViewBag.childrenViewModel = childrenViewModel;
            ViewBag.menViewModel = menViewModel;
            ViewBag.womenViewModel = womenViewModel;
            ViewBag.youngViewModel = youngViewModel;
            ViewBag.biblicalViewModel = biblicalViewModel;
            
            return View("Create");
        }


        //private async Task<ViewResult> ReloadDataAndReturnView()
        //{
        //    HashSet<SelectListItem> members = await GetAllMembers();
        //    ViewBag.childrenViewModel = new GroupBasedReportViewModel(ReportType.CRIANÇAS, members);
        //    ViewBag.menViewModel = new GroupBasedReportViewModel(ReportType.HOMENS, members);
        //    ViewBag.womenViewModel = new GroupBasedReportViewModel(ReportType.MULHERES, members);
        //    return View("Create");
        //}

    }
}