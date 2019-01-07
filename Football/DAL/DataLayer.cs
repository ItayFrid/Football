using Football.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Football.DAL
{
    public class DataLayer : DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Player>().ToTable("Players");
            modelBuilder.Entity<Staff>().ToTable("Staff");
            modelBuilder.Entity<Admin>().ToTable("Admins");
            modelBuilder.Entity<User>().ToTable("Users");
        }

        public DbSet<Player> players { get; set; }
        public DbSet<Staff> staffs { get; set; }
        public DbSet<Admin> admins { get; set; }
        public DbSet<User> users { get; set; }
    }
}