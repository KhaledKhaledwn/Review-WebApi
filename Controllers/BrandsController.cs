using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Infrastructure;
using ReviewApiApp.DataAccessLayer;
using ReviewApiApp.Domain;
using ReviewApiApp.Services;
using ReviewApiApp.ViewModels;
using System.Net;

namespace ReviewApiApp.Controllers
{
    [ApiController]

     [Route("api/products/{productId}/[controller]")]

    public class BrandsController : ControllerBase
    {
        private readonly ILogger<BrandsController> logger;
        private readonly IMailService mailservice;
        private readonly ProductionDataStore dataset;
        private readonly ProductRepository productrepository;

        public BrandsController(
              ILogger<BrandsController> _logger,
              IMailService _mailservice,
              ProductionDataStore dataset,
              ProductRepository productrepository)
        {
            this.logger = _logger;
            this.mailservice = _mailservice;
            this.dataset = dataset;
            this.productrepository = productrepository;
        }



        [HttpGet()]
        public async Task<ActionResult<List<Brand>>> GetBrands(int productId)
        {
            try
            {
                var production = dataset.Productions.FirstOrDefault(p => p.Id == productId);
                if (production == null)
                {
                    this.logger.LogInformation($"The production {productId} is not there.... sorry!");
                    return NotFound();
                }
                mailservice.Send("hello i am using localservice", " with DI concept ");
                return Ok(production.Brands);
            }

            catch (Exception ex)
            {
                this.logger.LogCritical("ther is an error");
                return StatusCode(500, "internal error because there is no production,plz call again ");
            }

            // var response = await productrepository.GetBrandsForProductionAsync(productId);
           


        }
        
        
        [HttpGet("{brandId}",Name ="GetBrand")]
        public ActionResult <Brand> GetBrandById(int productId, int brandId)
        {
            var production = dataset.Productions.FirstOrDefault( p => p.Id == productId);
            if (production == null)
                return NotFound("production is not available");

           var brand = production.Brands.FirstOrDefault(b => b.Id == brandId);
            if (brand == null)
                return NotFound("The Odered Brand is not here, please write a true id");
            return Ok(brand);
        }




        [HttpPost]
        public ActionResult<List<Brand>> CreateBrand(int productId, BrandForCreation brand)
        {
            var product = dataset.Productions.FirstOrDefault(p => p.Id == productId);
            if (product == null) return NotFound();


            int MaxId = dataset.Productions.SelectMany(p => p.Brands).Max(i => i.Id);
            var NewProductBrands = new Brand()
            {
                Name = brand.Name,
                Id = ++MaxId
            };

            product.Brands.Add(NewProductBrands);
            return CreatedAtRoute("GetBrand",new { productId = productId, brandId= NewProductBrands.Id},NewProductBrands);
        }


        [HttpPut("{BrandId}")] // Update all object// all fields
         public ActionResult UpdateBrand(int ProductId , int BrandId, BrandForUpdate brand)
        {
            var product = dataset.Productions.FirstOrDefault(i => i.Id == ProductId);
            if (product == null) return NotFound();

            var existingbrand = product.Brands.FirstOrDefault( b => b.Id == BrandId);
            if (existingbrand == null) return NotFound();

            // must using here Auto Mapper concept instead of using assignment by this way.
            existingbrand.Name = brand.Name;
            existingbrand.Description = brand.Description;

            return NoContent();
        }

     

        [HttpPatch("{BrandId}")] // Updating some of fields Not All Fields.
        public ActionResult ParitiallyUpdate(int productId, int BrandId, JsonPatchDocument<BrandForUpdate> patchdocument)
        {
            var existinigproduct = dataset.Productions.FirstOrDefault( i => i.Id == productId);
            if( existinigproduct == null) return NotFound();

            var existingbrand = existinigproduct.Brands.FirstOrDefault(b => b.Id == BrandId);
            if (existingbrand == null) return NotFound();

            // old values of object
            var BrandToPatch = new BrandForUpdate()
            {
                Name = existingbrand.Name,
                Description = existingbrand.Description,
            };

            patchdocument.ApplyTo(BrandToPatch, ModelState);
            if (!ModelState.IsValid)
                return BadRequest();// 404

            existingbrand.Name = BrandToPatch.Name;
            existingbrand.Description = BrandToPatch.Description;


            return NoContent();
            
        }




        [HttpDelete("{brandId}")]
        public ActionResult DeletBrand(int productId, int brandId)
        {
            var product = dataset.Productions.FirstOrDefault(i => i.Id == productId);
            if (product == null) return NotFound();

            var brand = product.Brands.FirstOrDefault(b => b.Id == brandId);
            if (brand == null) return NotFound();

            product.Brands.Remove(brand);
            return NoContent();
        }
    }
}

