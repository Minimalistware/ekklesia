using ekklesia.Models.Member;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ekklesia
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Member> Members { get; set; }

        public ApplicationContext(DbContextOptions options) : base(options)
        {
        }
    }
}
