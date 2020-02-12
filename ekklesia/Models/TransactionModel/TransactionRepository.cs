using ekklesia.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace ekklesia.Models.TransactionModel
{
    public interface ITransactionRepository
    {

        Transaction GetTransaction(int id);
        IEnumerable<Transaction> GetTransactions();
        Transaction Add(Transaction transaction);
        Transaction Update(Transaction transaction);
        Transaction Delete(int id);
        IEnumerable<Transaction> Search(TransactionSearchViewModel model);
    }
    public class TransactionRepository : ITransactionRepository
    {
        private readonly ApplicationContext applicationContext;

        public TransactionRepository(ApplicationContext applicationContext)
        {
            this.applicationContext = applicationContext;
        }

        public Transaction Add(Transaction transaction)
        {
            applicationContext.Transactions.Add(transaction);
            applicationContext.SaveChanges();
            return transaction;

        }

        public Transaction Delete(int id)
        {
            var transaction = applicationContext.Transactions.Find(id);
            if (transaction != null)
            {
                applicationContext.Transactions.Remove(transaction);
                applicationContext.SaveChanges();

            }
            return transaction;
        }

        public Transaction GetTransaction(int id)
        {
            return applicationContext.Transactions.Find(id);
        }

        public IEnumerable<Transaction> GetTransactions()
        {
            return applicationContext.Transactions;
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

        public Transaction Update(Transaction alteredTransaction)
        {
            var member = applicationContext.Transactions.Attach(alteredTransaction);
            member.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            applicationContext.SaveChanges();
            return alteredTransaction;
        }
    }
}
