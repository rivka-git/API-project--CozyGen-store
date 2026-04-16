using Dto;
using Model;

namespace Services
{
    public interface IUserServices
    {
        Task<DtoUserNameEmailRoleId> AddNewUser(DtoUserAll user);
        Task<DtoUserNameEmailRoleId?> GetUserById(int id);
        Task<IEnumerable<User>> GetUsers();
        Task<DtoUserNameEmailRoleId?> Login(DtoUserEmailPassword value);
        Task<DtoUserNameEmailRoleId> Update(int id, DtoUserAll value);
        Task<bool> IsAdminById(int id, string password);
    }
}

