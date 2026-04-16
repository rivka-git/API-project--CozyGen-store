using System.ComponentModel.DataAnnotations;

namespace Dto
{
    public record DtoCategoryAll(
        string Name,
        string Description,
        string ImageUrl
    );
}
