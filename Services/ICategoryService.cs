using Dto;


namespace Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<DtoCategoryNameId>> GetCategories();
        Task<DtoCategoryNameId> AddNewCategory(DtoCategoryAll newCategory);
        Task<DtoCategoryNameId> Delete(int id);
    }
}

