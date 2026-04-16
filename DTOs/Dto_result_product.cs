using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto
{

    public record DtoResultProduct(
        IEnumerable<DtoProductIdNameCategoryPriceDescImage> Products,
        int TotalCount
    );

}
//public class DtoResultProduct
//{
//    public IEnumerable<DtoProductIdNameCategoryPriceDescImage> Products { get; set; }
//    public int TotalCount { get; set; }
//}
