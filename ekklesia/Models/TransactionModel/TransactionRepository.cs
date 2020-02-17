using ekklesia.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ekklesia.Models.TransactionModel
{
    public interface ITransactionRepository : IFilling
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

        public IFilling Next { get; set; }

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

        public ReportCreateViewModel FillUpModel(ReportCreateViewModel model)
        {
            var trasaction = applicationContext.Transactions
                .Where(t => t.Date > DateTime.Today.AddMonths(-1));

            //Fill up previous month
            //TODO

            //Fill up income
            var income = trasaction
                .Where(t => t.Type == TransactionType.RECEITA)
                .Sum(t => t.Value);

            model.Income = income;

            //Fill up expense
            var expense = trasaction
                .Where(t => t.Type == TransactionType.DESPESA)
                .Sum(t => t.Value);
            model.Expense = expense;

            //Fill up tenth
            model.Tenth = trasaction
                .Where(t => t.Type == TransactionType.RECEITA)
                .Where(t => t.Category == "Dízimo")
                .Sum(t => t.Value);

            //Fill up balance
            model.Balance = income - expense;

            return Next != null ? Next.FillUpModel(model) : model;

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
            member.State = EntityState.Modified;
            applicationContext.SaveChanges();
            return alteredTransaction;
        }
    }
}
