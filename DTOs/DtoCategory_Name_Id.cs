using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace Dto
{
    public record DtoCategoryNameId(
        int CategoryId,
        string Name
    );
 
}

