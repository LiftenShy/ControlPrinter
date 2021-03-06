﻿using System.Data.Entity;
using CP.Data.Models;

namespace CP.Data
{
    public class ControlPrinterDbContext : DbContext
    {
        public ControlPrinterDbContext()
            : base("name=ControlPrinterDbContext")
        {
            Database.SetInitializer(new ControlPrinterDbInitializer());
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<StaticImage> StaticImages { get; set; }
        public DbSet<ResultImg> ResultImgs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>()
                .HasMany(u => u.Users)
                .WithRequired(r => r.Role)
                .HasForeignKey(f => f.RoleId);
        }
    }
}