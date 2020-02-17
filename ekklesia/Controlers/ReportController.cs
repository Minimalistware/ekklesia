using System;
using System.Collections.Generic;
using System.Linq;
using ekklesia.Models.EventModel;
using ekklesia.Models.MemberModel;
using ekklesia.Models.ReportModel;
using ekklesia.Models.TransactionModel;
using ekklesia.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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
        public ViewResult Create()
        {
            var model = CreateReportModel();
            return View(model);
        }

        private ReportCreateViewModel CreateReportModel()
        {
            var model = new ReportCreateViewModel();

            eventRepository.Next = memberRepository;
            memberRepository.Next = transactionRepository;
            eventRepository.FillUpModel(model);
            

            return model;
        }
               
    }
}