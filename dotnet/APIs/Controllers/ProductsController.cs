using Core.Entities;
using Infra.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

namespace APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        public StoreContext storeContext;
        public ProductsController(StoreContext Context)
        {
            this.storeContext = Context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return await storeContext.Products.ToListAsync();
        }

        [HttpGet("{Id:int}")]
        public async Task<ActionResult<Product>> GetProducts(int Id)
        {
            var product = await storeContext.Products.FirstOrDefaultAsync(x => x.Id == Id);

            if (product == null)
                return NotFound();

            return product;
        }

        [HttpPost]
        public async Task<ActionResult<Product>> CreateProducts(Product product)
        {
            await storeContext.Products.AddAsync(product);

            await storeContext.SaveChangesAsync();

            return product;
        }
        [HttpPut("{Id:int}")]
        public async Task<ActionResult> UpdateProducts(int Id, Product product)
        {
            if (Id != product.Id || !ProductExists(Id))
            {
                return BadRequest("Product Cannot be updated because it does not exist.");
            }
            storeContext.Entry(product).State = EntityState.Modified;
            await storeContext.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{Id:int}")]
        public async Task<ActionResult> DeleteProducts(int Id)
        {
            var product = await storeContext.Products.FindAsync(Id);

            if (product == null)
            {
                return NotFound();
            }

            storeContext.Products.Remove(product);
            await storeContext.SaveChangesAsync();

            return NoContent();
        }
        
        private bool ProductExists(int id)
        {
            return storeContext.Products.Any(e => e.Id == id);
        }   
    }
}
