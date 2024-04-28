using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using ProductData.Core.DTO;
using ProductData.Core.Interfaces;
using ProductsDomain;

namespace Products.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProduct _product;

        public ProductsController(IProduct product)
        {
            this._product = product;
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetAllProducts()
        {
            var products = await _product.GetAllProducts();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductById([FromRoute] Guid id)
        {
            var product = await _product.GetProductById(id);
            if(product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpGet("productsincategory/{CategoryId}")]
        public async Task<ActionResult<List<Product>>> GetAllProductsInCategory([FromRoute] Guid CategoryId)
        {
            var products = await _product.GetAllProductsinCategory(CategoryId);
            if(products is null)
            {
                return NotFound();
            }
            return Ok(products);
        }

        [HttpPost]
        public async Task<ActionResult<Product>> AddProduct([FromForm] ProductForCreate product)
        {
            if (product is null)
            {
                return BadRequest();
            }
            else
            {
                var resalut = await _product.AddProduct(product);
                return Ok(resalut);
            }
        }

        [HttpPut]
        public async Task<ActionResult<Product>> UpdateProduct([FromRoute] Guid id,[FromForm] Product product)
        {
            var resalut = await _product.UpdateProduct(id, product);
            if(resalut == null)
            {
                return NotFound();
            }
            return Ok(resalut);
        }

        [HttpPatch]
        public async Task<ActionResult<Product>> UpdateProductPatch([FromRoute] Guid id,[FromForm] JsonPatchDocument<Product> product)
        {
            var resalut = await _product.UpdateProductPatch(id, product);
            if (resalut == null)
            {
                return NotFound();
            }
            return Ok(resalut);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Product>> DeleteProduct([FromRoute] Guid id)
        {
            var productdeleted = await _product.DeleteProduct(id);
            return Ok(productdeleted);
        }

    }
}
