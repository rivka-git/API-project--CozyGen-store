using Dto;
using Microsoft.AspNetCore.Mvc;
using Services;
using System.Collections.Generic;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductService _productService;
        private readonly IUserServices _userService;

        public ProductController(IProductService productService, ILogger<ProductController> logger, IUserServices userService)
        {
            _productService = productService;
            _logger = logger;
            _userService = userService;
        }

        [HttpGet]
        public async Task<DtoResultProduct> GetProducts(
            [FromQuery] int position,
            [FromQuery] int skip,
            [FromQuery] string? desc,
            [FromQuery] int? minPrice,
            [FromQuery] int? maxPrice,
            [FromQuery] int?[] categoryIds,
            [FromQuery] int?[] styleIds)
        {
            return await _productService.GetProducts(position, skip, desc, minPrice, maxPrice, categoryIds, styleIds);
        }


      
        [HttpGet("{id}")]
        public async Task<DtoProductIdNameCategoryPriceDescImage> Get(int id)
        {
            return await _productService.GetById(id);
        }


    }
}
