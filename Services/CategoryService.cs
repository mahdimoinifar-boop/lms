using AutoMapper;
using LmsProject.DTOs.Category;
using LmsProject.Models.Entities;
using LmsProject.Repositories.Interfaces;
using LmsProject.Services.Interfaces;

namespace LmsProject.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepository<Category> _repository;
        private readonly IMapper _mapper;

        public CategoryService(IRepository<Category> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoryDto>> GetAllAsync()
        {
            var categories = await _repository.GetAllAsync();

            return _mapper.Map<IEnumerable<CategoryDto>>(categories);
        }

        public async Task<CategoryDto?> GetByIdAsync(int id)
        {
            var category =await _repository.GetByIdAsync(id);

            if (category == null)
                return null;

            return _mapper.Map<CategoryDto>(category);
        }

        public async Task<CategoryDto> CreateAsync(CreateCategoryDto dto)
        {
            var category =_mapper.Map<Category>(dto);

            await _repository.AddAsync(category);

            await _repository.SaveAsync();

            return _mapper.Map<CategoryDto>(category);
        }

        public async Task<bool> UpdateAsync(int id,CreateCategoryDto dto)
        {
            var category =await _repository.GetByIdAsync(id);

            if (category == null)
                return false;

            category.Name = dto.Name;

            _repository.Update(category);

            await _repository.SaveAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var category =await _repository.GetByIdAsync(id);

            if (category == null)
                return false;

            _repository.Delete(category);

            await _repository.SaveAsync();

            return true;
        }
    }
}