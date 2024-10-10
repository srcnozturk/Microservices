using Services.Catalog.API.Dtos;
using Services.Catalog.API.Models;
using Shared.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Catalog.API.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<Response<List<CategoryDto>>> GetAllAsync();
        Task<Response<CategoryDto>> CreateAsync(CategoryDto categoryDto);
        Task<Response<CategoryDto>> GetByIdAsync(string id);
    }
}
