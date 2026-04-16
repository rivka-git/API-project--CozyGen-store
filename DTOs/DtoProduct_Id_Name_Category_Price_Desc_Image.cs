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
    public record DtoProductIdNameCategoryPriceDescImage(
        int ProductId,
        string Name,
        int? CategoryId,
        string CategoryName,
        int? Price,
        int Stock,
        string Description,
        string FrontImageUrl,
        string BackImageUrl,
        ICollection<DtoProductStyle> ProductStyles
    );
}




