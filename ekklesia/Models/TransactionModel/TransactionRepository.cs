using ekklesia.Models.EventModel;
using ekklesia.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ekklesia.Models.TransactionModel
{
    public interface ITransactionRepository
    {

        Task<Transaction> GetTransaction(int id);
        IQueryable<Transaction> Transactions();
        Task<Transaction> GetTransactionWithOccasion(int id);
        Task<IList<Transaction>> GetTransactions();
        Task Add(Transaction transaction);
        Task Update(Transaction transaction);
        Task Delete(int id);
        IQueryable<Transaction> Search(TransactionSearchViewModel model);

    }
    public class TransactionRepository : ITransactionRepository
    {
        private readonly ApplicationContext applicationContext;

        public TransactionRepository(ApplicationContext applicationContext)
        {
            this.applicationContext = applicationContext;
        }


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

        public IQueryable<Transaction> Search(TransactionSearchViewModel model)
        {
            var query = "SELECT * FROM dbo.Transactions WHERE ";
            if (model.TransactionType != null)
            {
                query += "TransactionType = @p0 AND ";
            }
            if (model.Max != null)
            {
                query += "Value < @p1 AND ";
            }
            if (model.Min != null)
            {
                query += "Value > @p2 AND ";
            }
            if (model.Days != null)
            {
                query += "DAY([Date]) < @p3 AND ";
            }
            query += "1 = 1";
            return applicationContext.Transactions.FromSql(query, model.TransactionType, model.Max, model.Min, model.Days);

        }

        public async Task Update(Transaction alteredTransaction)
        {
            var member = applicationContext.Transactions.Attach(alteredTransaction);
            member.State = EntityState.Modified;
            await applicationContext.SaveChangesAsync();
        }

        public IQueryable<Transaction> Transactions()
        {
            return applicationContext.Transactions;
        }
    }
}
