using System.Globalization;
using Core.Data;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Infra.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

namespace APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(IGenericRepository<Product> productrepo) : ControllerBase
    {

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts(string? brand , string? type , string? sort)
        {
            var spec = new ProductSpecification(brand, type,sort);

            var products = await productrepo.ListAsync(spec);

            return Ok(products);
        }

        [HttpGet("{Id:int}")]
        public async Task<ActionResult<Product>> GetProducts(int Id)
        {
            var product = await productrepo.GetByIdAsync(Id);

            if (product == null)
                return NotFound();

            return product;
        }

        [HttpPost]
        public async Task<ActionResult<Product>> CreateProducts(Product product)
        {
            productrepo.Add(product);

            if (await productrepo.SaveAllAsync())
                return CreatedAtAction("GetProducts", new { id = product.Id }, product);

            return BadRequest("Product format is not correct");
        }
        [HttpPut("{Id:int}")]
        public async Task<ActionResult> UpdateProducts(int Id, Product product)
        {
            if (Id != product.Id || !productrepo.Exists(product.Id))
            {
                return BadRequest("Product Cannot be updated because it does not exist.");
            }
            productrepo.Update(product);
            if (await productrepo.SaveAllAsync())
                return NoContent();

            return BadRequest("Product format is not correct");
        }

        [HttpDelete("{Id:int}")]
        public async Task<ActionResult> DeleteProducts(int Id)
        {
            var product = await productrepo.GetByIdAsync(Id);
            
            if (product == null)
            {
                return NotFound();
            }
            productrepo.Remove(product);
            if (await productrepo.SaveAllAsync())
                return NoContent();

            return BadRequest("Product format is not correct");
        }
        [HttpGet("Brands")]
        public async Task<ActionResult<IEnumerable<string>>> GetBrands()
        {
            // todo
            return Ok();
        }
        [HttpGet("Types")]
        public async Task<ActionResult<IEnumerable<string>>> GetTypes()
        {
            // todo
            return Ok();
        }
        
        private bool ProductExists(int id)
        {
            return productrepo.Exists(id);
        }   
    }
}
