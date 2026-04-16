using Dto;
using Model;

namespace Services
{
    public interface IStyleService
    {
        Task<IEnumerable<DtoSyle_id_name>> GetStyles();
        Task<DtoSyle_id_name> AddNewStyle(DtoStyleAll newStyle);
        Task<DtoSyle_id_name> Delete(int id);
    }
}