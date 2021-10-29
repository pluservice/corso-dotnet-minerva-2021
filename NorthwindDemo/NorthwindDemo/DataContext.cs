using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using NorthwindDemo.Entities;

namespace NorthwindDemo
{
    public class DataContext : DbContext
    {
        private const string connectionString = @"Data Source=(localdb)\mssqllocaldb;Initial Catalog=Northwind;Integrated Security=True";

        public DbSet<Product> Products { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Supplier> Suppliers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString, options =>
            {
                options.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
            })
            .LogTo(Console.WriteLine, new[] { RelationalEventId.CommandExecuted });

            base.OnConfiguring(optionsBuilder);
        }
    }
}
