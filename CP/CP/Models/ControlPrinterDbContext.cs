using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CP.Models
{
    public class ControlPrinterDbContext : DbContext
    {
        public ControlPrinterDbContext()
            : base("name=ControlPrinterDbContext")
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Image> Images { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>()
                .HasMany(u => u.Users)
                .WithRequired(r => r.Role)
                .HasForeignKey(f => f.RoleId);
        }
    }
}