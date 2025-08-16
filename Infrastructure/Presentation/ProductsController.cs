using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.Attributes;
using Services.Abstraction;
using Shared;
using Shared.ErrorsModels;
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
        [ProducesResponseType(StatusCodes.Status200OK ,Type = typeof(PaginationResponse<ProductResultDto>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetails))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
        [Cache(100)]
        public async Task<ActionResult<PaginationResponse<ProductResultDto>>> GetAllProducts([FromQuery]ProductSpecificationsParamters specParams)
         {
            var result = await serviceManager.productService.GetAllProductsAsync(specParams);
           
            return Ok(result);
         }



        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductResultDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetails))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetails))]
        public async Task<ActionResult<ProductResultDto>> GetProductById(int id)
        {
            var result = await serviceManager.productService.GetProductByIdAsync(id);
           
            return Ok(result);
        }



        [HttpGet("brands")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<BrandResultDto>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetails))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
        public async Task<ActionResult<BrandResultDto>>GetAllBrands()
        {
            var result = await serviceManager.productService.GetAllBrandsAsync();
            if (result is null) return BadRequest();
            return Ok(result);
        }



        [HttpGet("types")]

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TypeResultDto>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetails))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
        public async Task<ActionResult<TypeResultDto>> GetAllTypes()
        {
            var result = await serviceManager.productService.GetAllTypesAsync();
            if (result is null) return BadRequest();
            return Ok(result);
        }

    }
}
