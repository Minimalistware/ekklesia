using Caelum.Stella.CSharp.Vault;
using ekklesia.Models.Member;
using ekklesia.Models.TransactionModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ekklesia
{

    public class ApplicationContext : DbContext
    {
        public DbSet<Member> Members { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        public ApplicationContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
        .Entity<Transaction>()
        .Property(t => t.Value)
        .HasConversion(
            v => (decimal)v,
            v => new Money(v));
        }
    }
}
