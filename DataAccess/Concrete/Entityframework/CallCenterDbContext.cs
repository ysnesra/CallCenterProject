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
        //parametreli constructor oluştururuz.DbContextOptions<CallCenterDbContext> türünde bir parametre almalı ve bu parametreyi DbContext'in temel yapıcısına aktarmalıdır.
        // eğer DbContext IoC den gelecekse bu mecburen olacak
        public CallCenterDbContext(DbContextOptions<CallCenterDbContext> options) : base(options)
        {

        }

        public DbSet<Call> Calls { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerRep> CustomerReps { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<RequestType> RequestTypes { get; set; }
        public DbSet<Status> Statuses { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=CallCenterDb;Trusted_Connection=True");
        //    }
        //}

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


            //modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            //bu koda gerek kalmadı.Her tablo için configuration classları oluşturup, database kolon isimlerini yazmıştım.Gerek olmadığını öğrenip kaldırdım.//Sadece Foreignkey vermem yeterli oldu.
        }
    }
}