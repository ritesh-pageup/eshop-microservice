using Discount.Grpc.Models;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Context
{
    public class DiscountDbContext:DbContext
    {
        public DiscountDbContext (DbContextOptions<DiscountDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Coupon>().HasData(
                new Coupon { Id=1, Amount=15000, Description="This is Laptop.",ProductId= "6728be4b845bfc04331cdd0a", ProductName="Laptop" },
                new Coupon { Id=2, Amount=999, Description="This is mouse.",ProductId= "67515e769d5bbbb480e693c6", ProductName="mouse" }
                );
        }
        public DbSet<Coupon> Coupons { get; set; }  
    }
}
