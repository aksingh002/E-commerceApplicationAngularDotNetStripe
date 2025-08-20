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
        public async Task<ActionResult<Product>> CreateProducts(Product product )
        {
            await storeContext.Products.AddAsync(product);

            await storeContext.SaveChangesAsync();

            return product;
        }
    }
}
