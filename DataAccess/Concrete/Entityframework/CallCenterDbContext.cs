using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;


namespace DataAccess.Concrete.Entityframework
{
    public class CallCenterDbContext : DbContext
    {
        public DbSet<Call> Calls { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerRep> CustomerReps { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<RequestType> RequestTypes { get; set; }
        public DbSet<Status> Statuses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=CallCenterDb;Trusted_Connection=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Call>().HasOne(a => a.Customer)
             .WithMany(a => a.Calls)
             .HasForeignKey(a => a.CustomerId)
             .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Call>().HasOne(a => a.CustomerRep)
               .WithMany(a => a.Calls)
               .HasForeignKey(a => a.CustomerRepId)
               .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Call>().HasOne(a => a.Request)
               .WithMany(a => a.Calls)
               .HasForeignKey(a => a.RequestId)
               .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}