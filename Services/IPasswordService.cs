
using Dto;


namespace Services
{
    public interface IPasswordService
    {
        int GetStrengthByPassword(string password);
    }
}