using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using ReviewApiApp.DataAccessLayer;
using ReviewApiApp.Domain;
using ReviewApiApp.Services;
using ReviewApiApp.ViewModels;
using System.Numerics;
using System.Text.Json;

namespace ReviewApiApp.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ProductionDataStore dataset;
        private readonly IProductRepository productrepo;
        private readonly IMapper map;
        public ProductController(
            ProductionDataStore dataset,
            IProductRepository productrepo,
            IMapper map)
        {
            this.dataset = dataset;
            this.productrepo = productrepo;
            this.map = map;
            
        }

        [HttpGet()]
        public async Task<ActionResult<List<ProductionSummary>>> GetAllProductions(int pageNumber =1, int pageSize = 10)
        {

            // var productions = await productrepo.GetProductionsAsync( pageNumber,  pageSize);

            var (productions,PaginationMD)  = await productrepo.GetProductionsAsync(pageNumber, pageSize);
            if (productions == null)
                return NotFound();

            // The First Way To Doing Mapper Between An Object And Another Objectl
            //var productinwithoutbrands = productions.Select(b => new ProductionSummary
            //{
            //    Id = b.Id,
            //    Name = b.Name
            //});
            //      var OutputVariable = map.Map<Desination> (Source);
            var productinwithoutbrands =  map.Map<List<ProductionSummary>>(productions);

            Response.Headers.Add("X-Pagination",JsonSerializer.Serialize(PaginationMD));
           

            return Ok(productinwithoutbrands);


        }

        [HttpGet("{ProductionId}")]
        public async Task<ActionResult<Production>> GetProduct(int ProductionId, bool WithBrands)
        {
            var response = await productrepo.GetProductionAsync(ProductionId, WithBrands);
            if (response == null)
                return NotFound();
            if(WithBrands)
            return Ok(response);
            return Ok(map.Map<ProductionSummary>(response));

        }

        // get production and after it .
        [HttpGet("nextpro/{ProductionId}")]
        public ActionResult<List<Production>> GetProductAndNextProduction(int ProductionId)
        {

            var production = dataset.Productions.FirstOrDefault(p => p.Id == ProductionId);

            var nextpro = dataset.Productions.FirstOrDefault(ne => ne.Id > ProductionId);

            var result = new List<Production>() { production, nextpro };
            if (production == null)
                return NotFound();
            if (nextpro == null)
                return NotFound();
            return Ok(result);

        }


        [HttpPost]
        public ActionResult<Production> CreateProduct(CreationForProductioncs product)
        {

            int NewId = dataset.Productions.Max(p => p.Id);
            Production NewProduct = new Production()
            {
                Name = product.Name,
                Brands = product.Brands,
                Id = ++NewId
            };

            dataset.Productions.Add(NewProduct);
            return Ok(NewProduct);

        }


        [HttpDelete("{ProductId}")]
        public async Task<ActionResult> DeletProduct(int ProductId)
        {
            var response = await productrepo.DeletProductAsync(ProductId);
            return response == false ? NotFound() : NoContent();

        }

        [HttpPut("{ProductId}")]// Updat all production's information
        public async Task<ActionResult> UpdateProduct(int ProductId, ProductForUpdating product)
        {
            var response = await productrepo.UpdateProductionAsyn(ProductId, product);
            return response == false ? NotFound() : NoContent();

        }

        [HttpPatch("{ProductId}")]
        public async Task<ActionResult> PartiallyUpdating(int ProductId, JsonPatchDocument<ProductForUpdating> productionpatch)
        {
            if (!ModelState.IsValid) return BadRequest();
            if (productionpatch == null) return BadRequest();

            var response = await productrepo.UpdatePartiallyAsync(ProductId, productionpatch);
            return !response ? NotFound() : NoContent();

        }

    } 
}
// Important Note: if you use where func will return [] if there is no production But
// FirstOrDefault it will return (Null) no []
