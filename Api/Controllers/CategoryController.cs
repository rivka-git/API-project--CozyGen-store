using Dto;
using Microsoft.AspNetCore.Mvc;
using Services;
using System.Collections.Generic;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ILogger<CategoryController> _logger;
        private readonly ICategoryService _s;
        private readonly IUserServices _userService;

        public CategoryController(ICategoryService i, ILogger<CategoryController> logger, IUserServices userService)
        {
            _s = i;
            _logger = logger;
            _userService = userService;
        }

        [HttpGet]
        public async Task<IEnumerable<DtoCategoryNameId>> Get()
        {
            return await _s.GetCategories();
        }
    }
}
