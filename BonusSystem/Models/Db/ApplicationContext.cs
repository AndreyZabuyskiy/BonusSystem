using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BonusSystem.Models.Db
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<BonusCard> BonusCards { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
