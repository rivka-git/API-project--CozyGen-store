using Dto;
using Model;

namespace Services
{
    public interface IStyleService
    {
        Task<IEnumerable<DtoStyleIdName>> GetStyles();
        Task<DtoStyleIdName> AddNewStyle(DtoStyleAll newStyle);
        Task<DtoStyleIdName> Delete(int id);
    }
}
