using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace server.Context
{
    public class AppContext : DbContext
    {
        public AppContext(DbContextOptions<AppContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Models.UserHasDesk>().HasKey(o => new { o.IdDesk, o.IdUser });
        }

        public DbSet<Models.User> Users { get; set; }

        public DbSet<Models.Desk> Desks { get; set; }

        public DbSet<Models.UserHasDesk> UserDesks { get; set; }
    }
}
