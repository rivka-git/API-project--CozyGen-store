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

        [HttpPost]
        public async Task<ActionResult<DtoProductIdNameCategoryPriceDescImage>> Post(
            [FromBody] DtoProductNameDescriptionPriceStockCategoryIdIsActiveStyleIds productDto,
            [FromHeader] int userId,
            [FromHeader] string password)
        {

            bool isAdmin = await _userService.IsAdminById(userId, password);
            if (!isAdmin)
            {
                return Forbid("גישה נדחתה: דרושות הרשאות מנהל לביצוע פעולה זו");
            }

            DtoProductIdNameCategoryPriceDescImage res = await _productService.AddNewProduct(productDto);
            if (res != null)
            {
                return Ok(res);
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<DtoProductIdNameCategoryPriceDescImage>> Delete(
            int id,
            [FromHeader] int userId,
            [FromHeader] string password)
        {

            bool isAdmin = await _userService.IsAdminById(userId, password);
            if (!isAdmin)
            {
                return Forbid("גישה נדחתה: דרושות הרשאות מנהל למחיקת מוצר");
            }

            DtoProductIdNameCategoryPriceDescImage res = await _productService.Delete(id);

            if (res != null)
            {
                return Ok(res);
            }
            return NotFound($"Product with ID {id} not found");
        }
        [HttpGet("{id}")]
        public async Task<DtoProductIdNameCategoryPriceDescImage> Get(int id)
        {
            return await _productService.GetById(id);
        }

        [HttpGet("by-ids")]
        public async Task<List<DtoProductIdNameCategoryPriceDescImage>> GetByIds([FromQuery] int[] ids)
        {
            var products = new List<DtoProductIdNameCategoryPriceDescImage>();

            foreach (var id in ids)
            {
                try
                {
                    var product = await _productService.GetById(id);
                    if (product != null)
                        products.Add(product);
                }
                catch
                {
                    // אם מוצר לא נמצא, פשוט נדלג עליו
                }
            }

            return products;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadProductWithImages(
            [FromHeader] int userId,
            [FromHeader] string password)
        {
            try
            {
                var form = await Request.ReadFormAsync();

                var frontImage = form.Files["frontImage"];
                var backImage = form.Files["backImage"];

                if (frontImage == null || backImage == null)
                    return BadRequest(new { message = "חובה להעלות שתי תמונות" });

                var name = form["name"].ToString();
                var description = form["description"].ToString();
                var price = decimal.Parse(form["price"].ToString());
                var categoryId = int.Parse(form["categoryId"].ToString());

                bool isAdmin = await _userService.IsAdminById(userId, password);
                if (!isAdmin)
                    return StatusCode(403, new { message = "אין הרשאות מנהל" });

                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "products");
                Directory.CreateDirectory(uploadsFolder);

                var frontFileName = $"{Guid.NewGuid()}_{frontImage.FileName}";
                var backFileName = $"{Guid.NewGuid()}_{backImage.FileName}";

                using (var stream = new FileStream(Path.Combine(uploadsFolder, frontFileName), FileMode.Create))
                    await frontImage.CopyToAsync(stream);

                using (var stream = new FileStream(Path.Combine(uploadsFolder, backFileName), FileMode.Create))
                    await backImage.CopyToAsync(stream);

                var productDto = new DtoProductNameDescriptionPriceStockCategoryIdIsActiveStyleIds(
                    name,
                    description,
                    price,
                    $"/uploads/products/{frontFileName}",
                    $"/uploads/products/{backFileName}",
                    new List<DtoStyleIdName>(),
                    0,
                    categoryId,
                    true
                );

                var result = await _productService.AddNewProduct(productDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "שגיאה: {Message}", ex.Message);
                return StatusCode(500, new { message = "שגיאה בהעלאת התמונות", error = ex.Message });
            }
        }
    }
}
