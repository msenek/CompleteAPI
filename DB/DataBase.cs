using System.Net.Http.Headers;
using Microsoft.EntityFrameworkCore;
using TestAPI.DB.Entities;

namespace TestAPI.DB
{
    public class TestAPIContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public TestAPIContext(DbContextOptions<TestAPIContext> options) : base(options)
        {
        }
    }
}
