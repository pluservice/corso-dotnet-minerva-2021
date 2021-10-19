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
    }
}
