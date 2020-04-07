using ekklesia.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ekklesia.Models.TransactionModel
{
    public interface ITransactionRepository : IFilling
    {

        Task<Transaction> GetTransaction(int id);
        Task<IList<Transaction>> GetTransactions();
        Task Add(Transaction transaction);
        Task Update(Transaction transaction);
        Task Delete(int id);
        IEnumerable<Transaction> Search(TransactionSearchViewModel model);

    }
    public class TransactionRepository : ITransactionRepository
    {
        private readonly ApplicationContext applicationContext;

        public TransactionRepository(ApplicationContext applicationContext)
        {
            this.applicationContext = applicationContext;
        }

        public IFilling Next { get; set; }

        public async Task Add(Transaction transaction)
        {
            await applicationContext.Transactions.AddAsync(transaction);
            await applicationContext.SaveChangesAsync();            
        }

        public async Task Delete(int id)
        {
            var transaction = await applicationContext.Transactions.FindAsync(id);
            if (transaction != null)
            {
                applicationContext.Transactions.Remove(transaction);
                applicationContext.SaveChanges();
            }            
        }

        public async Task<ReportCreateViewModel> FillUpGroupReportModel(GroupBasedReportViewModel model)
        {
            var trasaction = applicationContext.Transactions
                .Where(t => t.Date > DateTime.Today.AddMonths(-1));

            //Fill up previous month
            //TODO

            //Fill up income
            //var income = await trasaction
            //    .Where(t => t.Type == TransactionType.RECEITA)
            //    .SumAsync(t => t.Value);

            //model.Income = income;

            ////Fill up expense
            //var expense = await trasaction
            //    .Where(t => t.Type == TransactionType.DESPESA)
            //    .SumAsync(t => t.Value);
            //model.Expense = expense;

            ////Fill up tenth
            //model.Tenth = await trasaction
            //    .Where(t => t.Type == TransactionType.RECEITA)
            //    .Where(t => t.Category == "Dízimo")              
            //    .SumAsync(t => t.Value);

            //Fill up balance
            //model.Balance = income - expense;

            return Next != null ? await Next.FillUpGroupReportModel(model) : model;

        }

        public async Task<Transaction> GetTransaction(int id)
        {
            return await applicationContext.Transactions.FindAsync(id);
        }

        public async Task<IList<Transaction>> GetTransactions()
        {
            return await applicationContext.Transactions.ToListAsync();
        }

        public IEnumerable<Transaction> Search(TransactionSearchViewModel model)
        {
            var query = "SELECT * FROM dbo.Transactions WHERE ";
            if (model.Category != null)
            {
                query += "Category LIKE '%'+ @p0 +'%' AND ";
            }
            if (model.Type != null)
            {
                query += "Type = @p1 AND ";
            }
            query += "1 = 1";
            return applicationContext.Transactions.FromSql(query, model.Category, model.Type);
        }

        public async Task Update(Transaction alteredTransaction)
        {
            var member = applicationContext.Transactions.Attach(alteredTransaction);
            member.State = EntityState.Modified;
            await applicationContext.SaveChangesAsync();            
        }
    }
}
