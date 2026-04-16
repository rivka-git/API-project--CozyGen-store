using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dto;

namespace Services
{
    public interface IAiService
    {
        Task<DtoChatResponse> ChatAsync(string userMessage);

        Task<List<int>> AnalyzeImageAsync(
            IFormFile image,
            IEnumerable<DtoProductIdNameCategoryPriceDescImage> allProducts,
            IEnumerable<DtoStyleIdName> styles,
            IEnumerable<DtoCategoryNameId> categories
        );
    }
}
