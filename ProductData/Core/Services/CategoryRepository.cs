using Microsoft.EntityFrameworkCore;
using ProductData.Core.DTO;
using ProductData.Repository.Interfaces;
using ProductsDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductData.Core.Classes
{
    public class CategoryRepository : ICategory
    {
        private readonly AppDbContext _context;

        public CategoryRepository(AppDbContext context)
        {
            this._context = context;
        }

        public async Task<List<Category>> GetAllCategory()
        {

            var AllCategoreis = await _context.Categories.ToListAsync();
            if(AllCategoreis is null)
            {
                return null;
            }
            return AllCategoreis;
        }

        public async Task<Category> GetCategoryById(Guid id)
        {
            var Category = await _context.Categories.FindAsync(id);
            if (Category == null)
            {
                return null;
            }
            return Category;
        }
        public async Task<List<Category>> AddCategory(CategoryForCreate category)
        {
            Category maincategory = new Category();

            maincategory.Id = Guid.NewGuid();
            maincategory.Name = category.Name;
            _context.Categories.Add(maincategory);
            await _context.SaveChangesAsync();
            return await _context.Categories.ToListAsync();
            
        }

        public async Task<Category> UpdateCategory(Guid id, CategoryForCreate category)
        {
            var resalut = await _context.Categories.FindAsync(id);
            if (resalut != null)
            {
                return null;
            }
            resalut.Id = id;
            resalut.Name = category.Name;
            await _context.SaveChangesAsync();
            return resalut;
        }

        public async Task<Category> DeleteCategory(Guid id)
        {
            var result = await _context.Categories.FindAsync(id);
            if (result is null)
            {
                return null;
            }
            _context.Categories.Remove(result);
            await _context.SaveChangesAsync();
            return result;
        }
    }
}
