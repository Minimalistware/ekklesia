using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ekklesia.Models.EventModel;
using ekklesia.Models.TransactionModel;
using ekklesia.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ekklesia.Controlers
{
    public class TransactionController : Controller
    {
        private readonly ITransactionRepository repository;
        private readonly IEventRepository eventRepository;

        public TransactionController(ITransactionRepository repository, IEventRepository eventRepository)
        {
            this.repository = repository;
            this.eventRepository = eventRepository;
        }

        [AllowAnonymous]
        public async Task<ViewResult> List()
        {
            var transactions = await repository.GetTransactions();
            return View(transactions);
        }

        [HttpGet]
        public ViewResult Create()
        {
            var events = eventRepository.GetEvents();
            ViewBag.Revenue = new RevenueCreateViewModel();
            ViewBag.Expenditure = new ExpenditureCreateViewModel();
            return View("Create");
        }

        [HttpPost]
        public async Task<IActionResult> CreateRevenue(RevenueCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var revenue = new Revenue()
                {
                    Date = model.Date,
                    Value = model.Value,
                    RevenueType = model.RevenueType
                };
                await AddOcassion(model.OccasionId, revenue);
                await repository.Add(revenue);
                return RedirectToAction("list", "transaction");
            }
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> CreateExpenditure(ExpenditureCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var expenditure = new Expenditure()
                {
                    Date = model.Date,
                    Value = model.Value,
                    Description = model.Description,
                    Invoice = model.Invoice
                };
                await AddOcassion(model.OccasionId, expenditure);
                await repository.Add(expenditure);
                return RedirectToAction("list", "transaction");
            }
            return View();
        }

        [HttpGet]
        public async Task<ViewResult> Edit(int id)
        {
            var transaction = await repository.GetTransaction(id);
            if (transaction == null)
            {
                ViewBag.ErrorMessage = $"Transação com Id: {id} não pode ser encontrado";
                return View("NotFound");
            }
            switch (transaction.TransactionType)
            {
                case TransactionType.RECEITA:
                    var revenueEditViewModel = new RevenueEditViewModel(transaction as Revenue);
                    return View("EditRevenue", revenueEditViewModel);
                case TransactionType.DESPESA:
                    var expenditureEditViewModel = new ExpenditureEditViewModel(transaction as Expenditure);
                    return View("EditExpenditure", expenditureEditViewModel);
                default:
                    return View();
            }
        }

        [HttpPost]
        public async Task<IActionResult> EditRevenue(RevenueEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var revenue = await repository.GetTransaction(model.Id) as Revenue;
                revenue.Date = model.Date;
                revenue.Value = model.Value;
                revenue.RevenueType = model.RevenueType;
                await AddOcassion(model.OccasionId, revenue);
                await repository.Update(revenue);
                return RedirectToAction("list", "transaction");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> EditExpenditure(ExpenditureEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var expenditure = await repository.GetTransaction(model.Id) as Expenditure;
                expenditure.Date = model.Date;
                expenditure.Value = model.Value;
                expenditure.Description = model.Description;
                await AddOcassion(model.OccasionId, expenditure);
                await repository.Update(expenditure);
                return RedirectToAction("list", "transaction");
            }

            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public ViewResult Search()
        {
            return View();
        }

        [HttpPost]
        public ViewResult Search(TransactionSearchViewModel model)
        {
            var transactions = repository.Search(model);
            return View("List", transactions);
        }

        [HttpGet]
        public async Task<ViewResult> Details(int id)
        {
            var transaction = await repository.GetTransaction(id);
            var model = new TransactionDetailsViewModel(transaction);
            return View(model);
        }

        private async Task AddOcassion(int OccasionId, Transaction transaction)
        {
            var occasion = await eventRepository.GetEvent(OccasionId);
            if (occasion != null)
            {
                try
                {
                    transaction.Occasion = occasion;

                }
                catch (Exception ex)
                {
                    throw new Exception("Erro ao adicionar ocasião.", ex);
                }

            }
        }

        private async Task<HashSet<SelectListItem>> GetAllEvents()
        {
            var asyncmembers = await eventRepository.GetEvents();

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
    }
}