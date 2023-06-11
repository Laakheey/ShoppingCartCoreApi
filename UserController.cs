using Jwt.Model;
using Jwt.Model.Cart;
using Jwt.Model.Product_Model;
using Jwt.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Jwt.Controller
{

    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "User")]

    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private IGenericRepository<Product> genericRepository = null;

        public UserController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
            this.genericRepository = new GenericRepository<Product>();
        }


        [HttpGet("getproduct")]
        public async Task<IActionResult> GetProducts()
        {
            return Ok(genericRepository.GetAll());

        }

        [HttpGet("addedproduct")]
        public async Task<IActionResult> AddedProduct()
        {
            return Ok(dbContext.AddToCarts.ToList());

        }



        [HttpPost("AddToCart")]
        public async Task<IActionResult> AddToCart( AddToCart addToCart)
        {
            var product = await dbContext.Products.FindAsync(addToCart.Id);
            if (product != null && dbContext.AddToCarts==null)
            {
                addToCart.ProductName = product.Name;
                addToCart.ProductDescription = product.Description;
                addToCart.ProductPrice = product.Price;
                addToCart.ProductRating = product.Rating;
                addToCart.ProductQuantity = product.Quantity;
                await dbContext.AddToCarts.AddAsync(addToCart);
                await dbContext.SaveChangesAsync();
                return Ok(product);
            }
            else if (dbContext.AddToCarts.Any(X=>X.Id==product.Id))
            {
                return Ok();
            }
            else
            {
                addToCart.ProductName = product.Name;
                addToCart.ProductDescription = product.Description;
                addToCart.ProductPrice = product.Price;
                addToCart.ProductRating = product.Rating;
                addToCart.ProductQuantity = product.Quantity;
                await dbContext.AddToCarts.AddAsync(addToCart);
                await dbContext.SaveChangesAsync();
                return Ok(product);
            }
            
        }




        [HttpGet("Checkout")]
        public async Task<IActionResult> Checkout()
        {
            var allProductsInCart = await dbContext.AddToCarts.ToListAsync();
            if (allProductsInCart.Any())
            {
                dbContext.AddToCarts.RemoveRange(allProductsInCart);
                await dbContext.SaveChangesAsync();
                return Ok(allProductsInCart);
            }

            return NotFound();

        }










    }
}
