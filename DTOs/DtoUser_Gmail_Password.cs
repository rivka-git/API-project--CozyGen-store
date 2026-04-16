using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Dto
{
    public record DtoUserEmailPassword(
        string Email,
        string PasswordHash
    );
}

