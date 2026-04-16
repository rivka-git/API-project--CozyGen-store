using AutoMapper;
using Dto;
using Repository;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DtoCategoryNameId>> GetCategories()
        {
            var categories = await _categoryRepository.GetCategories();
            var categoryDtos = _mapper.Map<List<Category>, List<DtoCategoryNameId>>(categories);
            return categoryDtos;
        }

        public async Task<DtoCategoryNameId> AddNewCategory(DtoCategoryAll newCategory)
        {
            var categoryEntity = _mapper.Map<Category>(newCategory);

            var savedCategory = await _categoryRepository.AddNewCategory(categoryEntity);
            return _mapper.Map<DtoCategoryNameId>(savedCategory);

        }

        public async Task<DtoCategoryNameId> Delete(int id)
        {
            var savedCategory = await _categoryRepository.Delete(id);
            return _mapper.Map<DtoCategoryNameId>(savedCategory);

        }
    }
}

