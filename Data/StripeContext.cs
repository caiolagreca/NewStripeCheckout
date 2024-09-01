using Microsoft.EntityFrameworkCore;
using NewStripeApp.Models;

namespace NewStripeApp.Data
{
    public class StripeContext : DbContext
    {
        public StripeContext(DbContextOptions<StripeContext> options) : base(options) { }

        public DbSet<Item> Items { get; set; }
    }
}
