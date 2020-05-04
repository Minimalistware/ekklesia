using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ekklesia.Models.EventModel;
using ekklesia.Models.TransactionModel;
using ekklesia.Models.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ekklesiapi.resources
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionRepository repository;
        private readonly IEventRepository eventRepository;
        private readonly IHostingEnvironment hostingEnviroment;

        public TransactionController(ITransactionRepository repository, IEventRepository eventRepository,
            IHostingEnvironment hostingEnviroment)
        {
            this.repository = repository;
            this.eventRepository = eventRepository;
            this.hostingEnviroment = hostingEnviroment;
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

        [HttpPut]
        public async Task<IActionResult> UdpateRevenue(RevenueEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                
                var revenue = await repository.GetTransaction(model.Id) as Revenue;
                if (revenue == null)
                {
                    return NotFound(model.Id);
                }
                revenue.Date = model.Date;
                revenue.Value = model.Value;
                revenue.RevenueType = model.RevenueType;
                await repository.Update(revenue);
                
                return Ok(revenue.ToJson());
            }
            return BadRequest(ModelState);
        }

        [HttpPut]
        public async Task<IActionResult> UdpateExpenditure(ExpenditureEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                
                var expenditure = await repository.GetTransaction(model.Id) as Expenditure;
                if (expenditure == null)
                {
                    return NotFound(model.Id);
                }
                expenditure.Date = model.Date;
                expenditure.Value = model.Value;
                expenditure.Description = model.Description;
                
                await repository.Update(expenditure);
                
                return Ok(expenditure.ToJson());
            }
            return BadRequest(ModelState);
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
                return Ok(revenue.ToJson());
            }
            return BadRequest(ModelState);
        }


        [HttpPost]
        public async Task<IActionResult> CreateExpenditure(ExpenditureCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                //string uniqueFileName = ProcessUploadedFile(model.Invoice);
                var expenditure = new Expenditure()
                {
                    Date = model.Date,
                    Value = model.Value,
                    Description = model.Description,
                    //Invoice = uniqueFileName
                };
                await AddOcassion(model.OccasionId, expenditure);
                await repository.Add(expenditure);
                return RedirectToAction("list", "transaction");
            }
            return BadRequest(ModelState);
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