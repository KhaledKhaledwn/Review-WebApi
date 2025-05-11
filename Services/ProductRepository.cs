using DataAccessLayer.DbContexts;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using ReviewApiApp.Domain;
using ReviewApiApp.ViewModels;

namespace ReviewApiApp.Services
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApiReviewDbContext context;

        public ProductRepository(ApiReviewDbContext context)
        {
            this.context = context;
        }
        public async Task<(List<Production>, PaginationMetaData)> GetProductionsAsync(int pageNumber, int pageSize)
        {

            var ItemsCount = await context.Products.CountAsync();
            var PaginantioMetaData = new PaginationMetaData(pageNumber, pageSize, ItemsCount);

             var products = await context.Products
                                        .OrderBy(n => n.Name)
                                        .Skip(pageSize * (pageNumber - 1)) // 10 *1 = 10
                                        .Take(pageSize)
                                        .ToListAsync();
            return (products, PaginantioMetaData);

        }
        public async Task<Production> GetProductionAsync(int ProductionId, bool withBrands)
        {
            if (withBrands)
                return await context.Products.Include(b => b.Brands).FirstOrDefaultAsync(p => p.Id == ProductionId);
            return await context.Products.FirstOrDefaultAsync(p => p.Id == ProductionId);

        }

        public async Task<bool> DeletProductAsync(int ProductionId)
        {
            var product = await context.Products.FirstOrDefaultAsync(i => i.Id == ProductionId);
            if (product == null) return false;
            context.Products.Remove(product);
            await context.SaveChangesAsync();
            return true;

        }

        public async Task<bool> UpdateProductionAsyn(int Productid, ProductForUpdating productObject)
        {
            var response = await context.Products.FirstOrDefaultAsync(i => i.Id == Productid);
            if (response == null) return false;
            // if there is a prodcut with ProductId.

            response.Name = productObject.Name;
            response.Brands = productObject.Brands;
            // response.Brands.Clear();  // Clear existing brands
            // response.Brands.AddRange(productObject.Brands);
            // response.Brands = productObject.Brands;

            // response.Brands = productObject.Brands.Select(b => new Brand { Id = b.Id, Name = b.Name }).ToList();

            //context.Entry(response).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdatePartiallyAsync(int ProductionId, JsonPatchDocument<ProductForUpdating> productionpatch)
        {
            if (productionpatch is null)
                return false;

            var response = await context.Products.FirstOrDefaultAsync( i => i.Id == ProductionId);
            if (response is null) return false;

            // Old Data
            var ProdcutionToPatch = new ProductForUpdating
            {
                Name = response.Name, 
                Brands = response.Brands 
            };

            productionpatch.ApplyTo(ProdcutionToPatch);
            
            // update the data
            response.Name = ProdcutionToPatch.Name;
            response.Brands = ProdcutionToPatch.Brands;

            await context.SaveChangesAsync();

            return true;
        }
    }
}
