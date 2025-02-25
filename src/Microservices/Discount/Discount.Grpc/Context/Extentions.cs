using Discount.Grpc.Services;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Context
{
    public static class Extentions
    {
        public static IApplicationBuilder UseMigration(this IApplicationBuilder app) {
            using var scope = app.ApplicationServices.CreateScope();
            using var dbContext = scope.ServiceProvider.GetRequiredService<DiscountDbContext>();
            dbContext.Database.MigrateAsync();
            return app;
        }
    }
}
