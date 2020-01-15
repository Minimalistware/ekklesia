﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ekklesia.Models.Transaction
{
    public interface ITransactionRepository
    {

        Transaction GetTransaction(int id);
        IEnumerable<Transaction> GetTransactions();
        Transaction Add(Transaction transaction);
        Transaction Update(Transaction transaction);
        Transaction Delete(int id);

    }
    public class TransactionRepository:ITransactionRepository
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

        public Transaction Update(Transaction alteredTransaction)
        {
            var member = applicationContext.Transactions.Attach(alteredTransaction);
            member.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            applicationContext.SaveChanges();
            return alteredTransaction;
        }
    }
}
