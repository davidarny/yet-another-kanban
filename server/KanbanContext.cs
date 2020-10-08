using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace server
{
    public class KanbanContext : DbContext
    {
        public KanbanContext(DbContextOptions<KanbanContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Models.UserHasDesk>().HasKey(o => new { o.IdDesk, o.IdUser });
        }

        public DbSet<Models.User> Users { get; set; }

        public DbSet<Models.UserHasDesk> UserHasDesks { get; set; }

        public DbSet<Models.Desk> Desks { get; set; }

        public DbSet<Models.DeskColumn> DeskColumns { get; set; }

        public DbSet<Models.Card> Cards { get; set; }
    }
}
