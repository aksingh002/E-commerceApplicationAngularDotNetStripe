using System.Globalization;
using Core.Data;
using Core.Entities;
using Core.Interfaces;
using Infra.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

namespace APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(IProductRepository productrepo) : ControllerBase
    {

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts(string? brand , string? type , string? sort)
        {
            var products = await productrepo.GetProductsAsync( brand , type, sort);

            return Ok(products);
        }

        [HttpGet("{Id:int}")]
        public async Task<ActionResult<Product>> GetProducts(int Id)
        {
            var product = await productrepo.GetProductByIdAsync(Id);

            if (product == null)
                return NotFound();

            return product;
        }

        [HttpPost]
        public async Task<ActionResult<Product>> CreateProducts(Product product)
        {
            productrepo.AddProductsAsync(product);

            if (await productrepo.SaveChangesAsync())
                return CreatedAtAction("GetProducts", new { id = product.Id }, product);

            return BadRequest("Product format is not correct");
        }
        [HttpPut("{Id:int}")]
        public async Task<ActionResult> UpdateProducts(int Id, Product product)
        {
            if (Id != product.Id || !productrepo.ProductExists(product.Id))
            {
                return BadRequest("Product Cannot be updated because it does not exist.");
            }
            productrepo.UpdateProductsAsync(product);
            if (await productrepo.SaveChangesAsync())
                return NoContent();

            return BadRequest("Product format is not correct");
        }

        [HttpDelete("{Id:int}")]
        public async Task<ActionResult> DeleteProducts(int Id)
        {
            var product = await productrepo.GetProductByIdAsync(Id);
            
            if (product == null)
            {
                return NotFound();
            }
            productrepo.DeleteProductsAsync(product);
            if (await productrepo.SaveChangesAsync())
                return NoContent();

            return BadRequest("Product format is not correct");
        }
        [HttpGet("Brands")]
        public async Task<ActionResult<IEnumerable<string>>> GetBrands()
        {
            return Ok(await productrepo.GetBrandsAsync());
        }
        [HttpGet("Types")]
        public async Task<ActionResult<IEnumerable<string>>> GetTypes()
        {
            return Ok(await productrepo.GetTypesAsync());
        }
        
        private bool ProductExists(int id)
        {
            return productrepo.ProductExists(id);
        }   
    }
}
