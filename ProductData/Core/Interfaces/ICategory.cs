using ProductData.Core.DTO;
using ProductsDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductData.Repository.Interfaces
{
    public interface ICategory
    {
        public Task<List<Category>> GetAllCategory();
        public Task<Category> GetCategoryById(Guid id);
        public Task<List<Category>> AddCategory(CategoryForCreate category);
        public Task<Category> DeleteCategory(Guid id);
        public Task<Category> UpdateCategory(Guid id, CategoryForCreate category);
    }
}
