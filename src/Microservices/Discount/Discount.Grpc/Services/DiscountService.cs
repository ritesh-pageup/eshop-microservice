using Discount.Grpc.Context;
using Discount.Grpc.Models;
using Grpc.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Services
{
    public class DiscountService(DiscountDbContext dbContext, ILogger<DiscountService> logger) 
        :DiscountProtoService.DiscountProtoServiceBase
    {
        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            logger.LogInformation("GetDiscount method called for ProductName: {ProductName}", request.ProductName);

            var coupon = await dbContext.Coupons.FirstOrDefaultAsync(x => x.ProductName == request.ProductName);
            if (coupon is null)
            {
                logger.LogWarning("Coupon not found for ProductName: {ProductName}", request.ProductName);
                return new CouponModel { Id = 0, ProductName = "No Product Found", Description = "No Description", Amount = 0 };
            }

            logger.LogInformation("Coupon found for ProductName: {ProductName}", request.ProductName);
            return coupon.Adapt<CouponModel>();
        }

        public override async Task<CouponModel> CreateDiscount(CreateDicountRequest request, ServerCallContext context)
        {
            logger.LogInformation("CreateDiscount method called with ProductName: {ProductName}", request.Coupon.ProductName);

            var coupon = request.Coupon.Adapt<Coupon>();
            dbContext.Coupons.Add(coupon);
            await dbContext.SaveChangesAsync();

            logger.LogInformation("Coupon created successfully for ProductName: {ProductName}", request.Coupon.ProductName);
            return coupon.Adapt<CouponModel>();
        }

        public override async Task<CouponModel> UpdateDiscount(UpdateDicountRequest request, ServerCallContext context)
        {
            logger.LogInformation("UpdateDiscount method called for Coupon Id: {CouponId}", request.Coupon.Id);

            var coupon = await dbContext.Coupons.FirstOrDefaultAsync(x => x.Id == request.Coupon.Id);
            if (coupon is null)
            {
                logger.LogWarning("Coupon not found for Id: {CouponId}", request.Coupon.Id);
                return new CouponModel { Id = 0, ProductName = "No Product Found", Description = "No Description", Amount = 0 };
            }

            coupon.ProductName = request.Coupon.ProductName;
            coupon.Amount = (decimal)request.Coupon.Amount;
            coupon.ProductId = request.Coupon.ProductId;
            coupon.Description = request.Coupon.Description;

            dbContext.Coupons.Update(coupon);
            await dbContext.SaveChangesAsync();

            logger.LogInformation("Coupon updated successfully for Id: {CouponId}", request.Coupon.Id);
            return request.Coupon;
        }

        public override async Task<DeleteDicountResponse> DeleteDiscount(DeleteDicountRequest request, ServerCallContext context)
        {
            logger.LogInformation("DeleteDiscount method called for ProductName: {ProductName}", request.ProductName);

            var coupon = await dbContext.Coupons.FirstOrDefaultAsync(x => x.ProductName == request.ProductName);
            if (coupon is null)
            {
                logger.LogWarning("Coupon not found for ProductName: {ProductName}", request.ProductName);
                return new DeleteDicountResponse { Success = false };
            }

            dbContext.Coupons.Remove(coupon);
            await dbContext.SaveChangesAsync();

            logger.LogInformation("Coupon deleted successfully for ProductName: {ProductName}", request.ProductName);
            return new DeleteDicountResponse { Success = true };
        }
    }
}
