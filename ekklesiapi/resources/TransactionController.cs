using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ekklesia.Models.TransactionModel;
using Microsoft.AspNetCore.Mvc;

namespace ekklesiapi.resources
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionRepository repository;

        public TransactionController(ITransactionRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public async Task<IEnumerable<object>> Browse()
        {
            var transactions = await repository.GetTransactions();
            return transactions.Select(t => t.ToJson()).ToList();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Read(int id)
        {
            var transaction = await repository.GetTransaction(id);
            if (transaction == null)
            {
                return NotFound(id);
            }
            return Ok(transaction.ToJson());
        }

        //    [HttpPut]
        //    public async Task<IActionResult> Udpate(TransactionEditViewModel model)
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            var transaction = await repository.GetTransaction(model.Id);
        //            if (transaction == null)
        //            {
        //                return NotFound(model.Id);
        //            }
        //            transaction.Date = model.Date;
        //            transaction.Value = model.Value;
        //            transaction.Type = model.Type;
        //            transaction.Category = model.Category;

        //            await repository.Update(transaction);
        //            return Ok(transaction.ToJson());
        //        }
        //        return BadRequest(ModelState);
        //    }

        //    [HttpPost]
        //    public async Task<IActionResult> Add(TransactionCreateViewModel model)
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            Transaction transaction = new Transaction()
        //            {
        //                Date = model.Date,
        //                Value = model.Value,
        //                Type = model.Type,
        //                Category = model.Category
        //            };
        //            await repository.Add(transaction);
        //            return Ok(transaction.ToJson());
        //        }
        //        return BadRequest(ModelState);
        //    }
    }
}