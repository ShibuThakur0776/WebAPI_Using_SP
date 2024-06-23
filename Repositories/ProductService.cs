using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;
using WebAPI_Using_SP.Data;
using WebAPI_Using_SP.Models;
using WebAPI_Using_SP.Repositories;

namespace WebAPI_Using_SP.Repositories
{
    public class ProductService : IProductService
    {
        private readonly DbContextClass _dbContext;
        public ProductService(DbContextClass dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<int> AddProductAsync(Product product)
        {
            var parameter = new List<SqlParameter>();
            parameter.Add(new SqlParameter("@ProductName", product.ProductName));
            parameter.Add(new SqlParameter("@ProductDescription", product.ProductDescription));
            parameter.Add(new SqlParameter("@ProductPrice", product.ProductPrice));
            parameter.Add(new SqlParameter("ProductStock", product.ProductStock));

            var result = await Task.Run(() => _dbContext.Database.ExecuteSqlRawAsync
            (@"exec AddNewProduct @ProductName, @ProductDescription, @ProductPrice, @ProductStock", parameter.ToArray()));

            return result;
        }

        public async Task<int> DeleteProductAsync(int Id)
        {
            return await Task.Run(() => _dbContext.Database.ExecuteSqlInterpolatedAsync($"DeletePrductByID {Id}"));
        }

        public async Task<IEnumerable<Product>> GetProductByIdAsync(int Id)
        {
            var param = new SqlParameter("@ProductId", Id);
            var productDetails = await Task.Run(() => _dbContext.Products.FromSqlRaw(@"exec GetProductByID @ProductId", param).ToListAsync());

            return productDetails;
        }

        public async Task<List<Product>> GetProductListAsync()
        {
            return await _dbContext.Products.FromSqlRaw<Product>("GetProductList").ToListAsync();
        }

        public async Task<int> UpdateProductAsync(Product product)
        {
            var paramater = new List<SqlParameter>();

            paramater.Add(new SqlParameter("@ProductId", product.ProductId));
            paramater.Add(new SqlParameter("@ProductName", product.ProductName));
            paramater.Add(new SqlParameter("@ProductDescription", product.ProductDescription));
            paramater.Add(new SqlParameter("@ProductPrice", product.ProductPrice));
            paramater.Add(new SqlParameter("@ProductStock", product.ProductStock));

            var result = await Task.Run(() => _dbContext.Database.
            ExecuteSqlRawAsync
            (@"exec UpdateProduct @ProductId, @ProductName, @ProductDescription, @ProductPrice, @ProductStock",
            paramater.ToArray()));
            return result;
        }
    }
}
