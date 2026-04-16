using Dto;
using Microsoft.AspNetCore.Mvc;


namespace Services
{
    public interface IProductService
    {
        Task<DtoProductIdNameCategoryPriceDescImage> AddNewProduct(DtoProductNameDescriptionPriceStockCategoryIdIsActiveStyleIds productDto);
        Task<DtoResultProduct> GetProducts([FromQuery] int position,
           [FromQuery] int skip,
           [FromQuery] string? desc,
           [FromQuery] int? minPrice,
           [FromQuery] int? maxPrice,
           [FromQuery] int?[] categoryIds,
           [FromQuery] int?[] styleIds);
        Task<DtoProductIdNameCategoryPriceDescImage> Delete(int id);
        Task<DtoProductIdNameCategoryPriceDescImage> GetById(int id);

    }

}
