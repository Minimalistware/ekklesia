using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ekklesia.Models.EventModel;
using ekklesia.Models.TransactionModel;
using ekklesia.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ekklesia.Controlers
{
    public class TransactionController : Controller
    {
        private readonly ITransactionRepository repository;
        private readonly IEventRepository eventRepository;
        private readonly IHostingEnvironment hostingEnviroment;

        public TransactionController(ITransactionRepository repository, IEventRepository eventRepository
            , IHostingEnvironment hostingEnviroment)
        {
            this.repository = repository;
            this.eventRepository = eventRepository;
            this.hostingEnviroment = hostingEnviroment;
        }

        [AllowAnonymous]
        public async Task<ViewResult> List()
        {
            var transactions = await repository.GetTransactions();
            return View(transactions);
        }

        [HttpGet]
        public async Task<ViewResult> Create()
        {
            var events = await GetEvents();
            ViewBag.Revenue = new RevenueCreateViewModel(events);
            ViewBag.Expenditure = new ExpenditureCreateViewModel(events);
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
                string uniqueFileName = ProcessUploadedFile(model.Invoice);
                var expenditure = new Expenditure()
                {
                    Date = model.Date,
                    Value = model.Value,
                    Description = model.Description,
                    Invoice = uniqueFileName
                };
                await AddOcassion(model.OccasionId, expenditure);
                await repository.Add(expenditure);
                return RedirectToAction("list", "transaction");
            }
            return View();
        }

        //TODO REMOVER OS CONDICIONAIS
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
                    return View("NotFound");
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
            var transaction = await repository.GetTransactionWithOccasion(id);
            if (transaction == null)
            {
                ViewBag.ErrorMessage = $"Transação com Id: {id} não pode ser encontrado";
                return View("NotFound");
            }
            switch (transaction.TransactionType)
            {
                case TransactionType.RECEITA:
                    var revenue = new RevenueDetailsViewModel(transaction as Revenue);
                    return View("RevenueDetails", revenue);
                case TransactionType.DESPESA:
                    var expenditure = new ExpenditureDetailsViewModel(transaction as Expenditure);
                    return View("ExpenditureDetails", expenditure);
                default:
                    return View("NotFound");
            }

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

        private async Task<HashSet<SelectListItem>> GetEvents(int eventsNumber = 10)
        {
            var asyncevents = await eventRepository.GetEvents();

            var eventsList = asyncevents
                            .OrderBy(e => e.Date)
                            .ToList()
                            .Take(eventsNumber);



            HashSet<SelectListItem> occasions = new HashSet<SelectListItem>();
            foreach (var item in eventsList)
            {
                occasions.Add(new SelectListItem
                {
                    Value = item.Id.ToString(),
                    Text = $"{item.EventType} {item.Date}"

                });
            }

            return occasions;
        }

        //TODO MÉTODO DUPLICADO
        private string ProcessUploadedFile(IFormFile file)
        {
            string uniqueFileName = null;
            if (file != null)
            {
                var uploadsFolder = Path.Combine(hostingEnviroment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }

            }

            return uniqueFileName;
        }
    }
}