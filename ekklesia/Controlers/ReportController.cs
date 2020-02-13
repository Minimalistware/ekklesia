using System;
using System.Collections.Generic;
using System.Linq;
using ekklesia.Models.MemberModel;
using ekklesia.Models.ReportModel;
using ekklesia.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ekklesia.Controlers
{
    public class ReportController : Controller
    {
        private readonly IReportRepository repository;
        private readonly IMemberRepository memberRepository;

        public ReportController(IReportRepository repository, IMemberRepository memberRepository)
        {
            this.repository = repository;
            this.memberRepository = memberRepository;
        }

        public IActionResult List()
        {
            var reports = repository.GetReports();
            return View(reports);
        }

        [HttpGet]
        public ViewResult Create()
        {
            var model = new ReportCreateViewModel();            
            return View(model);
        }

        private ReportCreateViewModel ReportFillUp(ReportCreateViewModel model)
        {
            model.AllMembers = GetAllMembers();
            return null;
        }

        private List<SelectListItem> GetAllMembers()
        {
            var memberList = memberRepository
                            .GetMembers()
                            .OrderBy(m => m.Name)
                            .ToList();



            List<SelectListItem> members = new List<SelectListItem>();
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
    }
}