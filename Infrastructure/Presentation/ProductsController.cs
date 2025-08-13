using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController(IServiceManager serviceManager) : ControllerBase
    {
         [HttpGet]
         public async Task<IActionResult> GetAllProducts(int? brandId, int? typeId,string? sort)
         {
            var result = await serviceManager.productService.GetAllProductsAsync(brandId, typeId,sort);
            if (result is null) return BadRequest();
            return Ok(result);
         }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var result = await serviceManager.productService.GetProductByIdAsync(id);
            if (result is null) return NotFound();//404 
            return Ok(result);
        }

        [HttpGet("brands")]
        public async Task<IActionResult>GetAllBrands()
        {
            var result = await serviceManager.productService.GetAllBrandsAsync();
            if (result is null) return BadRequest();
            return Ok(result);
        }
        [HttpGet("types")]
        public async Task<IActionResult> GetAllTypes()
        {
            var result = await serviceManager.productService.GetAllTypesAsync();
            if (result is null) return BadRequest();
            return Ok(result);
        }

    }
}
