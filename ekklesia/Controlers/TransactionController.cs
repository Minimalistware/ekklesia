using System;
using ekklesia.Models.TransactionModel;
using ekklesia.Models.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace ekklesia.Controlers
{
    public class TransactionController : Controller
    {
        private readonly ITransactionRepository repository;
        private readonly IHostingEnvironment hostingEnviroment;

        public TransactionController(ITransactionRepository repository, IHostingEnvironment hostingEnviroment)
        {
            this.repository = repository;
            this.hostingEnviroment = hostingEnviroment;
        }


        public ViewResult List()
        {
            var transactions = repository.GetTransactions();
            return View(transactions);
        }

        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(TransactionCreateViewModel model)
        {
            Console.WriteLine(model.Value);
            if (ModelState.IsValid)
            {
                Console.WriteLine(model.Value.ToString());
                Transaction transaction = new Transaction()
                {
                    Date = model.Date,
                    Value = model.Value,
                    Type = model.Type,
                    Category = model.Category
                };
                repository.Add(transaction);
                return RedirectToAction("list", "transaction");
            }
            return View();
        }

        [HttpGet]
        public ViewResult Edit(int id)
        {
            var transaction = repository.GetTransaction(id);
            var model = new TransactionEditViewModel(transaction);
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(TransactionEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var transaction = repository.GetTransaction(model.Id);
                transaction.Date = model.Date;
                transaction.Value = model.Value;
                transaction.Type = model.Type;
                transaction.Category = model.Category;

                repository.Update(transaction);
                return RedirectToAction("list", "transaction");
            }

            return View();
        }
    }
}