using ekklesia.Models.ReportModel;
using ekklesia.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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

        public async Task<IActionResult> List()
        {
            var reports = await repository.GetReports();
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
            reportBuilder.FilloutGroupReport(menViewModel);
            reportBuilder.FilloutGroupReport(womenViewModel);

            ViewBag.childrenViewModel = childrenViewModel;
            ViewBag.menViewModel = menViewModel;
            ViewBag.womenViewModel = womenViewModel;
            ViewBag.youngViewModel = youngViewModel;
            ViewBag.biblicalViewModel = biblicalViewModel;

            return View("Create");
        }


        [HttpPost]
        public async Task<IActionResult> CreateGroupBasedReport(GroupBasedReportViewModel model)
        {
            if (ModelState.IsValid)
            {
                var groupBasedReport = new GroupBasedReport(model);
                await repository.Add(groupBasedReport);
                return RedirectToAction("list", "report");

            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCellBasedReport(GroupBasedReportViewModel model)
        {
            if (ModelState.IsValid)
            {
                var groupBasedReport = new GroupBasedReport(model);
                await repository.Add(groupBasedReport);
                return RedirectToAction("list", "report");

            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateBiblicalBasedReport(BiblicalBasedReportViewModel model)
        {
            if (ModelState.IsValid)
            {
                var biblicalBasedReport = new BiblicalBasedReport(model);
                await repository.Add(biblicalBasedReport);
                return RedirectToAction("list", "report");

            }
            return View();
        }

        public async Task<ViewResult> Details(int? id)
        {
            var report = await repository.GetReport(id.Value);
            if (report == null)
            {
                Response.StatusCode = 404;
                return View("EventNotFound", id.Value);
            }
            switch (report.ReportType)
            {
                case ReportType.CRIANÇAS:
                    var childrenViewModel = new GroupBasedReportDetailsViewModel((GroupBasedReport)report);
                    return View("GroupBasedReportDetails", childrenViewModel);
                case ReportType.MULHERES:
                    var womenViewModel = new GroupBasedReportDetailsViewModel((GroupBasedReport)report);
                    return View("GroupBasedReportDetails", womenViewModel);
                case ReportType.HOMENS:
                    var menViewModel = new GroupBasedReportDetailsViewModel((GroupBasedReport)report);
                    return View("GroupBasedReportDetails", menViewModel);
                case ReportType.BIBLÍCO:
                    var childrenViewModel2 = new GroupBasedReportViewModel((GroupBasedReport)report);
                    return View("GroupBasedReport", childrenViewModel2);
                case ReportType.CÉLULA:
                    var childrenViewModel23 = new GroupBasedReportViewModel((GroupBasedReport)report);
                    return View("GroupBasedReport", childrenViewModel23);
                default:
                    return View();
            }


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