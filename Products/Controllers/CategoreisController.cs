using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ProductData.Core.DTO;
using ProductData.Repository.Interfaces;
using ProductsDomain;

namespace Products.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoreisController : ControllerBase
    {
        private readonly ICategory _category;

        public CategoreisController(ICategory category)
        {
            this._category = category;
        }

        [HttpGet]
        public async Task<ActionResult<List<Category>>> GetAllCategory()
        {
            var resalut = await _category.GetAllCategory();
            if(resalut is null)
            {
                return NotFound();
            }
            return Ok(resalut);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategoryById([FromRoute] Guid id)
        {
            var resalut = await _category.GetCategoryById(id);
            if(resalut is null)
            {
                return NotFound();
            }
            return Ok(resalut);
        }

        [HttpPost]
        public async Task<ActionResult<List<Category>>> AddCategory([FromBody] CategoryForCreate category)
        {
            var resalut = await _category.AddCategory(category);
            return Ok(resalut);
        }

        [HttpPut]
        public async Task<ActionResult<Category>> UpdateCategory([FromRoute] Guid id, [FromBody] CategoryForCreate category)
        {
            var resalut = await _category.UpdateCategory(id, category);
            return Ok(resalut);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Category>> DelteCategory(Guid id)
        {
            var resalut = await _category.DeleteCategory(id);
            return Ok();
        }


    }
}
