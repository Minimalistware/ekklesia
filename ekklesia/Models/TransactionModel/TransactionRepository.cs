using ekklesia.Models.EventModel;
using ekklesia.Models.ReportModel;
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
        Task<Transaction> GetTransactionWithOccasion(int id);
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


        public async Task<Transaction> GetTransaction(int id)
        {
            return await applicationContext.Transactions.FindAsync(id);
        }

        public async Task<Transaction> GetTransactionWithOccasion(int id)
        {
            return await applicationContext.Transactions
                .Include(t => t.Occasion)
                .SingleOrDefaultAsync(t => t.Id == id);
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

        public async Task<ReportCreateViewModel> FillOutBaseReport(ReportCreateViewModel model)
        {
            var cults = applicationContext.Occasions
                             .OfType<Cult>()
                             .Where(c => c.CultType.ToString() == model.Type.ToString());

            var trasaction = from c in cults
                             join t in applicationContext.Transactions on c.Id equals t.OccasionId
                             where t.Date > DateTime.Today.AddMonths(-1)
                             select t;


            //Fill out income
            var income = await trasaction
                .Where(t => t.TransactionType == TransactionType.RECEITA)
                .SumAsync(t => t.Value);

            model.Income = income;

            //Fill out expense
            var expense = await trasaction
                .Where(t => t.TransactionType == TransactionType.DESPESA)
                .SumAsync(t => t.Value);

            model.Expense = expense;

            //Fill out tenth

            trasaction = from c in cults
                         join t in applicationContext.Transactions.OfType<Revenue>()
                         .Where(t => t.RevenueType == RevenueType.DÍZIMO)
                         on c.Id equals t.OccasionId
                         where t.Date > DateTime.Today.AddMonths(-1)
                         select t;

            model.Tenth = await trasaction.SumAsync(t => t.Value);

            //Fill out balance
            model.Balance = income - expense;

            return Next != null ? await Next.FillOutBaseReport(model) : model;
        }


    }
}
