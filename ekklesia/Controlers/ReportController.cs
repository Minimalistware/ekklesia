using ekklesia.Models.EventModel;
using ekklesia.Models.MemberModel;
using ekklesia.Models.ReportModel;
using ekklesia.Models.TransactionModel;
using ekklesia.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ekklesia.Controlers
{
    public class ReportController : Controller
    {
        private readonly IReportRepository repository;
        private readonly IMemberRepository memberRepository;
        private readonly IEventRepository eventRepository;
        private readonly ITransactionRepository transactionRepository;

        public ReportController(IReportRepository repository,
            IMemberRepository memberRepository,
            IEventRepository eventRepository,
            ITransactionRepository transactionRepository)
        {
            this.repository = repository;
            this.memberRepository = memberRepository;
            this.eventRepository = eventRepository;
            this.transactionRepository = transactionRepository;
        }

        public IActionResult List()
        {
            var reports = repository.GetReports();
            return View(reports);
        }

        [HttpGet]
        public async Task<ViewResult> Create()
        {
            var model = new GroupBasedReportViewModel(ReportType.CRIANÇAS);
            eventRepository.Next = transactionRepository;
            transactionRepository.Next = memberRepository;

            //Executes the pattern.
            await eventRepository.FillOutBaseReport(model);

            ViewBag.childrenViewModel = model;
            ViewBag.menViewModel = new GroupBasedReportViewModel(ReportType.HOMENS);
            ViewBag.womenViewModel = new GroupBasedReportViewModel(ReportType.MULHERES);
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