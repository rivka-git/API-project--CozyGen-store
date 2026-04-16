using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto
{
    public record DtoStyleIdName(
        int StyleId,
        string Name,
        string Description,
        string ImageUrl
    );
}



