using Microsoft.AspNetCore.Mvc;
using DotnetCoding.Core.Models;
using DotnetCoding.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using DotnetCoding.Core.Models.Dto;

namespace DotnetCoding.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        public readonly IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// Get the list of product
        /// </summary>
        /// <returns></returns>
        [HttpPost("List")]
        public async Task<IActionResult> GetProductList([FromBody] ProductSearchRequestDto requestModel)
        {
            var productList = await _productService.GetFilteredProducts(requestModel);
            if (productList == null)
            {
                return NotFound();
            }
            return Ok(productList);
        }

        /// <summary>
        /// Get product by id
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        [HttpGet("{productId}")]
        public async Task<IActionResult> GetProductById(int productId)
        {
            var product = await _productService.GetProductById(productId);

            if (product != null)
            {
                return Ok(product);
            }
            else
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Add a new product
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPost("Create")]
        public async Task<IActionResult> CreateProduct(Product product)
        {
            // Check if the price is more than $10,000
            if (product.Price > 10000)
            {
                return BadRequest("Product creation not allowed. Price exceeds $10,000.");
            }           

            var createdProductId = await _productService.CreateProduct(product);

            if (createdProductId > 0)
            {
                return Ok(true);
            }
            else
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Update the product
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPut("Update")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(400)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> UpdateProduct(Product product)
        {
            if (product != null)
            {
                var productData = await _productService.GetProductById(product.ProductId);
                if(productData == null) 
                {
                    return NotFound();
                }

                var isProductCreated = await _productService.UpdateProduct(product);
                if (isProductCreated)
                {
                    return NoContent();
                }
                return BadRequest();
            }
            else
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Delete product by id
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        [HttpDelete("{productId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(400)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> DeleteProduct(int productId)
        {
            var isProductCreated = await _productService.DeleteProduct(productId);

            if (isProductCreated)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}