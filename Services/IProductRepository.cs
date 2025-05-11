using Azure;
using Microsoft.AspNetCore.JsonPatch;
using ReviewApiApp.Domain;
using ReviewApiApp.ViewModels;

namespace ReviewApiApp.Services
{
    public interface IProductRepository
    {
        public Task<(List<Production>, PaginationMetaData)> GetProductionsAsync(int pageNumber, int pageSize);
        public Task<Production> GetProductionAsync(int ProductionId, bool withBrands );

        public Task<bool> UpdateProductionAsyn(int ProductId, ProductForUpdating productObject);

        public Task<bool> UpdatePartiallyAsync(int ProductionId, JsonPatchDocument <ProductForUpdating> productionpatch );
       public Task<bool> DeletProductAsync(int ProductionId);

    }
}
