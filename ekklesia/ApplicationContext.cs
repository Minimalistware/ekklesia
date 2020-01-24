using Caelum.Stella.CSharp.Vault;
using ekklesia.Models;
using ekklesia.Models.EventModel;
using ekklesia.Models.MemberModel;
using ekklesia.Models.TransactionModel;
using Microsoft.EntityFrameworkCore;


namespace ekklesia
{

    public class ApplicationContext : DbContext
    {
        public DbSet<Member> Members { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Event> Events { get; set; }

        //public DbSet<SundaySchool> SundaySchools { get; set; }
        //public DbSet<Reunion> Reunions { get; set; }
        //public DbSet<Cult> Cults { get; set; }


        public ApplicationContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SundaySchool>();
            modelBuilder.Entity<Reunion>();
            modelBuilder.Entity<Cult>();

            modelBuilder.Entity<MeetingMember>().HasKey(
                mm =>
                    new { mm.MemberId, mm.EventId }
                    );


            modelBuilder
                .Entity<Transaction>()
                .Property(t => t.Value)
                .HasConversion(v => (decimal)v, v => new Money(v));
        }
    }
}
