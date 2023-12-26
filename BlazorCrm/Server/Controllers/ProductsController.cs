using BlazorCrm.Server.Data;
using BlazorCrm.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazorCrm.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly DataContext _context;
        public ProductsController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetAllProducts()
        {
            return await _context.Products
                .Where(c => !c.IsDeleted)
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            var result = await _context.Products.FindAsync(id);
            if (result is null)
            {
                return NotFound("Contact not found.");
            }

            return result;
        }
        [HttpPost]
        public async Task<ActionResult<List<Product>>> CreateProducts(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Product>> UpdateProduct(int id, Product product)
        {
            var dbProduct = await _context.Products.FindAsync(id);
            if (dbProduct is null)
            {
                return NotFound("Contact not found.");
            }

            dbProduct.ProductName = product.ProductName;
            dbProduct.ProductCode = product.ProductCode;
            dbProduct.Price = product.Price;
            dbProduct.Availability = product.Availability;
            dbProduct.ProductImage = product.ProductImage;
            dbProduct.Weight = product.Weight;
            dbProduct.Dimensions = product.Dimensions;
            dbProduct.Text = product.Text;
            dbProduct.DateUpdated = DateTime.Now;

            await _context.SaveChangesAsync();
            return Ok(product);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Product>>> DeleteProduct(int id)
        {
            var dbProduct = await _context.Products.FindAsync(id);
            if (dbProduct is null)
            {
                return NotFound("Contact not found.");
            }
            dbProduct.IsDeleted = true;
            dbProduct.DateDeleted = DateTime.Now;
            await _context.SaveChangesAsync();
            return await GetAllProducts();
        }

    }
}
