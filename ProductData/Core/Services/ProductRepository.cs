using Azure;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using ProductData.Core.DTO;
using ProductData.Core.Interfaces;
using ProductsDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductData.Core.Classes
{
    public class ProductRepository : IProduct
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            this._context = context;
        }
        public async Task<List<Product>> GetAllProducts()
        {
            var products = await _context.Products.ToListAsync();
            if(products is null)
            {
                return null;
            }
            return products;
        }

        public async Task<Product> GetProductById(Guid id)
        {
            var product = await _context.Products.FindAsync(id);
            if(product is null)
            {
                return null;
            }
            return product;
        }

        public async Task<Product> AddProduct(ProductForCreate product)
        {
            using var stream = new MemoryStream();
            await product.Img.CopyToAsync(stream);

            Product newProduct = new Product();
            if(product is null)
            {
                return null;
            }
            newProduct.Id = Guid.NewGuid();
            newProduct.Name = product.Name;
            newProduct.price = product.price;
            newProduct.Note = product.Note;
            newProduct.CategoryId = product.CategoryId;
            newProduct.Img = stream.ToArray();

            await _context.Products.AddAsync(newProduct);
            await _context.SaveChangesAsync();
            return newProduct;
        }

        public async Task<Product> UpdateProduct(Guid id , Product product)
        {
            var newproduct = await _context.Products.FindAsync(id);
            if(newproduct is null)
            {
                return null;
            }
            newproduct.Id = id;
            newproduct.Name = product.Name;
            newproduct.price = product.price;
            newproduct.Note = product.Note;
            newproduct.Img = product.Img;
            newproduct.CategoryId = product.CategoryId;
            _context.SaveChanges();
            return newproduct;

        }

        public async Task<Product> UpdateProductPatch(Guid id, JsonPatchDocument<Product> product)
        {
            var resalut = await _context.Products.FindAsync(id);
            if(resalut is null)
            {
                return null;
            }
            product.ApplyTo(resalut);
            await _context.SaveChangesAsync();
            return resalut;
        }

        public async Task<Product> DeleteProduct(Guid id)
        {
            var resalut = await _context.Products.FindAsync(id);
            if(resalut is null)
            {
                return null;
            }
            _context.Products.Remove(resalut);
            await _context.SaveChangesAsync();
            return resalut;
        }

        public async Task<List<Product>> GetAllProductsinCategory(Guid categoryId)
        {
            var resalut = await _context.Products.Where(p => p.CategoryId == categoryId).ToListAsync();

            if(resalut is null)
            {
                return null;
            }
            return resalut;
        }
    }
}
