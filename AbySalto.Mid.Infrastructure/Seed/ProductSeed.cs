using AbySalto.Mid.Application.Interfaces;
using AbySalto.Mid.Domain.Entities;
using AbySalto.Mid.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AbySalto.Mid.Infrastructure.Seed
{
    public class ProductSeed
    {
        private readonly IProductService _productService;
        private readonly DatabaseContext _context;

        public ProductSeed(IProductService productService, DatabaseContext context)
        {
            _productService = productService;
            _context = context;
        }
        public static async Task SeedProductsAsync(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var productService = scope.ServiceProvider.GetRequiredService<IProductService>();
                var context = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
                var productSeed = new ProductSeed(productService, context);
                await productSeed.SeedProductsInternalAsync();
            }
        }

        private async Task SeedProductsInternalAsync()
        {
            var products = await _productService.GetProductsAsync();

            if (products?.Products != null)
            {
                foreach (var product in products.Products)
                {
                    var existingProduct = await _context.Products
                        .FirstOrDefaultAsync(p => p.Id == product.Id);

                    if (existingProduct == null)
                    {
                        _context.Products.Add(new Product
                        {
                            Title = product.Title,
                            Description = product.Description,
                            Category = product.Category,
                            Price = product.Price,
                            DiscountPercentage = product.DiscountPercentage,
                            Rating = product.Rating,
                            Stock = product.Stock,
                            Brand = product.Brand,
                            Sku = product.Sku,
                            Weight = product.Weight,
                            WarrantyInformation = product.WarrantyInformation,
                            ShippingInformation = product.ShippingInformation,
                            AvailabilityStatus = product.AvailabilityStatus,
                            ReturnPolicy = product.ReturnPolicy,
                            MinimumOrderQuantity = product.MinimumOrderQuantity,
                            Thumbnail = product.Thumbnail,
                            Images = product.Images
                        });
                    }
                    else
                    {
                        existingProduct.Title = product.Title;
                        existingProduct.Description = product.Description;
                        existingProduct.Category = product.Category;
                        existingProduct.Price = product.Price;
                        existingProduct.DiscountPercentage = product.DiscountPercentage;
                        existingProduct.Rating = product.Rating;
                        existingProduct.Stock = product.Stock;
                        existingProduct.Brand = product.Brand;
                        existingProduct.Sku = product.Sku;
                        existingProduct.Weight = product.Weight;
                        existingProduct.WarrantyInformation = product.WarrantyInformation;
                        existingProduct.ShippingInformation = product.ShippingInformation;
                        existingProduct.AvailabilityStatus = product.AvailabilityStatus;
                        existingProduct.ReturnPolicy = product.ReturnPolicy;
                        existingProduct.MinimumOrderQuantity = product.MinimumOrderQuantity;
                        existingProduct.Thumbnail = product.Thumbnail;
                        existingProduct.Images = product.Images;

                        _context.Products.Update(existingProduct);
                    }
                }
                await _context.SaveChangesAsync();
            }
        }
    }
}
