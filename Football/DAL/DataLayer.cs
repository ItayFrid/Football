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
        }

        public DbSet<Player> players { get; set; }
        public DbSet<Staff> staffs { get; set; }
    }
}