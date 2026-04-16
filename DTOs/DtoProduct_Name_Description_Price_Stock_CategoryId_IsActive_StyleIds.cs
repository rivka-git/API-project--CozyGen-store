using Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto
{
    public record DtoProductNameDescriptionPriceStockCategoryIdIsActiveStyleIds(
        string Name,
        string Description,
        decimal Price,
        string FrontImageUrl,
        string BackImageUrl,
        List<DtoStyleIdName> ProductStyles,
        int Stock,
        int CategoryId,
        bool IsActive
    );
}



