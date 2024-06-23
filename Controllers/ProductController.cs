using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI_Using_SP.Models;
using WebAPI_Using_SP.Repositories;

namespace WebAPI_Using_SP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly IProductService productService;
        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }
        [HttpGet("getproductlist")]
        public async Task<List<Product>> GetProductListAsync()
        {
            try
            {
                return await productService.GetProductListAsync();
            }
            catch
            {
                throw;
            }
        }
        [HttpGet("getproductbyid")]
        public async Task<IEnumerable<Product>> GetProductByIdAsync(int Id)
        {
            try
            {
                var response = await productService.GetProductByIdAsync(Id);
                if (response == null)
                {
                    return null;
                }
                return response;
            }
            catch
            {
                throw;
            }
        }
        [HttpPost("addproduct")]
        public async Task<IActionResult> AddProductAsync(Product product)
        {
            if (product == null)
            {
                return BadRequest();
            }
            try
            {
                var response = await productService.AddProductAsync(product);
                return Ok(response);
            }
            catch
            {
                throw;
            }
        }
        [HttpPut("updateproduct")]
        public async Task<IActionResult> UpdateProductAsync(Product product)
        {
            if (product == null)
            {
                return BadRequest();
            }
            try
            {
                var result = await productService.UpdateProductAsync(product);
                return Ok(result);
            }
            catch
            {
                throw;
            }
        }
        [HttpDelete("deleteproduct")]
        public async Task<int> DeleteProductAsync(int Id)
        {
            try
            {
                var response = await productService.DeleteProductAsync(Id);
                return response;
            }
            catch
            {
                throw;
            }
        }
    }
}
