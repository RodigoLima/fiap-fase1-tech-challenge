using fiap_fase1_tech_challenge.DTOs.GameLibrary;
using fiap_fase1_tech_challenge.Models;

namespace fiap_fase1_tech_challenge.Services.Interfaces
{
    public interface IGameLibraryService
    {
        Task<IEnumerable<GameLibrary>> GetAllAsync();
        Task<GameLibrary?> GetByIdAsync(int id);
        Task<GameLibrary> CreateAsync(GameLibraryCreateRequest gameLibrary);
        Task<bool> UpdateAsync(int id,GameLibraryUpdateRequest gameLibrary);
        Task<bool> DeleteAsync(int id);
    }
}
