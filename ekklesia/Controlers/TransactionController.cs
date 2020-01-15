using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ekklesia.Models.Transaction;
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

            return View();
        }

        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(TransactionCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                Transaction transaction = new Transaction()
                {
                    Date = model.Date,
                    Value = model.Value,
                    Type = model.Type,
                    Category = model.Category
                };
                repository.Add(transaction);
                return RedirectToAction("list","transaction");
            }
            return View();
        }
    }
}