using Microsoft.EntityFrameworkCore;
using DotnetCoding.Core.Models;

namespace DotnetCoding.Infrastructure
{
    public class DbContextClass : DbContext
    {
        public DbContextClass(DbContextOptions<DbContextClass> contextOptions) : base(contextOptions)
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductApprovalQueue> ProductApprovalQueues { get; set; }
    }
}