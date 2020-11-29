using ekklesia.Models;
using ekklesia.Models.ReportModel;
using ekklesia.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
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

        [AllowAnonymous]
        public async Task<ViewResult> List(int pageNumber = 1)
        {
            var paginatedList = await PaginatedList<Report>.CreateAsync(repository.Reports(), pageNumber, 10);
            return View(paginatedList);
        }

        [HttpGet]
        public ViewResult Create()
        {
            var childrenViewModel = new GroupBasedReportViewModel(ReportType.CRIANÇAS);
            var menViewModel = new GroupBasedReportViewModel(ReportType.HOMENS);
            var womenViewModel = new GroupBasedReportViewModel(ReportType.MULHERES);
            var youngViewModel = new YoungBasedReportViewModel();
            var biblicalViewModel = new BiblicalBasedReportViewModel();

            return View("Create");
        }

        [HttpGet]
        public async Task<ViewResult> CreateChildrenReport()
        {
            var model = new GroupBasedReportViewModel(ReportType.CRIANÇAS);
            model = await reportBuilder.FilloutGroupReport(model);
            return View("CreateChildrenReport", model);
        }

        [HttpGet]
        public async Task<ViewResult> CreateMenReport()
        {
            var model = new GroupBasedReportViewModel(ReportType.HOMENS);
            model = await reportBuilder.FilloutGroupReport(model);
            return View("CreateMenReport", model);
        }

        [HttpGet]
        public async Task<ViewResult> CreateWomenReport()
        {
            var model = new GroupBasedReportViewModel(ReportType.MULHERES);
            model = await reportBuilder.FilloutGroupReport(model);
            return View("CreateWomenReport", model);
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

        [HttpGet]
        [AllowAnonymous]
        public ViewResult Search()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ViewResult> Search(ReportSearchViewModel model)
        {
            var reports = repository.Search(model);
            var paginatedList = await PaginatedList<Report>.CreateAsync(reports, 1, 5);
            return View("List", paginatedList);
        }


    }
}