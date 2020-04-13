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
            HashSet<SelectListItem> members = await GetAllMembers();
            var model = new GroupBasedReportViewModel(ReportType.CRIANÇAS, members);
            eventRepository.Next = transactionRepository;
            //Executes the pattern.
            await eventRepository.CompleteBaseReportFor(model);

            ViewBag.childrenViewModel = model;
            ViewBag.menViewModel = new GroupBasedReportViewModel(ReportType.HOMENS, members);
            ViewBag.womenViewModel = new GroupBasedReportViewModel(ReportType.MULHERES, members);
            return View("Create");
        }

        private ReportCreateViewModel CreateReportModel()
        {
            var model = new GroupBasedReportViewModel();
            /*Defaines a Chain of Reponsability.
             https://en.wikipedia.org/wiki/Chain-of-responsibility_pattern
             */

            eventRepository.Next = transactionRepository;

            //Executes the pattern.
            eventRepository.CompleteBaseReportFor(model);

            return model;
        }

        private async Task<HashSet<SelectListItem>> GetAllMembers()
        {
            var asyncmembers = await memberRepository
                            .GetMembers();

            var memberList = asyncmembers
                            .OrderBy(m => m.Name)
                            .ToList();


            HashSet<SelectListItem> members = new HashSet<SelectListItem>();
            foreach (var item in memberList)
            {
                members.Add(new SelectListItem
                {
                    Value = item.Id.ToString(),
                    Text = item.Name

                });
            }

            return members;
        }

        private async Task<ViewResult> ReloadDataAndReturnView()
        {
            HashSet<SelectListItem> members = await GetAllMembers();
            ViewBag.childrenViewModel = new GroupBasedReportViewModel(ReportType.CRIANÇAS, members);
            ViewBag.menViewModel = new GroupBasedReportViewModel(ReportType.HOMENS, members);
            ViewBag.womenViewModel = new GroupBasedReportViewModel(ReportType.MULHERES, members);
            return View("Create");
        }

    }
}