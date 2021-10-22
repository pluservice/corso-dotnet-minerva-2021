using Microsoft.EntityFrameworkCore;
using SampleWebApi.DataAccessLayer.Entities;

namespace SampleWebApi.DataAccessLayer
{
    public class DataContext : DbContext
    {
        public DbSet<Person> People { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<Person>(model =>
            //{
            //    model.Property(p => p.FirstName).IsRequired();
            //});
        }
    }
}
