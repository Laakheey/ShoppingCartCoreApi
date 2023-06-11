using Jwt.Model;
using Jwt.Model.Product_Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Jwt.Controller
{
    [ApiController]
    [Authorize(Roles="Admin")]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        public AdminController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet("getproduct")]
        public async Task<IActionResult> GetProducts()
        {
            return Ok(await dbContext.Products.ToListAsync());

        }



        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetProduct([FromRoute] Guid id)
        {
            var product = await dbContext.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);

        }


        [HttpPost("addproduct")]
        public async Task<IActionResult> AddProduct(AddProductRequest addProductRequest)
        {
            var product = new Product()
            {
                Id = Guid.NewGuid(),
                Name = addProductRequest.Name,
                Description = addProductRequest.Description,
                Price = addProductRequest.Price,
                Rating = addProductRequest.Rating,
                Category=addProductRequest.Category,
                Quantity=addProductRequest.Quantity,
            };
            await dbContext.Products.AddAsync(product);
            await dbContext.SaveChangesAsync();
            return Ok(product);
        }

        [HttpPut("{id:guid}", Name = "EditProduct")]
        public async Task<IActionResult> EditProduct([FromRoute] Guid id,UpdateProductRequest updateProductRequest)
        {
            var product = await dbContext.Products.FindAsync(id);
            if (product != null)
            {
                product.Name = updateProductRequest.Name;
                product.Description = updateProductRequest.Description;
                product.Price = updateProductRequest.Price;
                product.Rating = updateProductRequest.Rating;
                product.Category = updateProductRequest.Category;
                product.Quantity = updateProductRequest.Quantity;
                await dbContext.SaveChangesAsync();
                return Ok(product);
            }
            return NotFound();
        }





        [HttpDelete("{id:guid}", Name = "DeleteProduct")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var product = await dbContext.Products.FindAsync(id);
            if (product != null)
            {
                dbContext.Products.Remove(product);
                await dbContext.SaveChangesAsync();
                return Ok(product);
            }

            return NotFound();
        }






    }
}
