using Microsoft.AspNetCore.JsonPatch;
using ProductData.Core.DTO;
using ProductsDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductData.Core.Interfaces
{
    public interface IProduct
    {
        public Task<List<Product>> GetAllProducts();
        public Task<List<Product>> GetAllProductsinCategory(Guid id);
        public Task<Product> GetProductById(Guid id);
        public Task<Product> AddProduct(ProductForCreate product);
        public Task<Product> UpdateProduct(Guid id , Product product);
        public Task<Product> UpdateProductPatch(Guid id , JsonPatchDocument<Product> product);
        public Task<Product> DeleteProduct(Guid id);
    }
}
