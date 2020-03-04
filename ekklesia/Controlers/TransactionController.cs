using System.Threading.Tasks;
using ekklesia.Models.TransactionModel;
using ekklesia.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ekklesia.Controlers
{
    public class TransactionController : Controller
    {
        private readonly ITransactionRepository repository;

        public TransactionController(ITransactionRepository repository)
        {
            this.repository = repository;
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
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(TransactionCreateViewModel model)
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
                await repository.Add(transaction);
                return RedirectToAction("list", "transaction");
            }
            return View();
        }

        [HttpGet]
        public async Task<ViewResult> Edit(int id)
        {
            var transaction = await repository.GetTransaction(id);
            var model = new TransactionEditViewModel(transaction);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(TransactionEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var transaction = await repository.GetTransaction(model.Id);
                transaction.Date = model.Date;
                transaction.Value = model.Value;
                transaction.Type = model.Type;
                transaction.Category = model.Category;

                await repository.Update(transaction);
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
    }
}