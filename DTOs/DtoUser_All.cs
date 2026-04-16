using System.ComponentModel.DataAnnotations;

namespace Dto
{
    public record DtoUserAll(
        int UserId,
        string Email,
        string FirstName,
        string LastName,
        string PasswordHash,
        string Phone,
        string Address
    );
}

