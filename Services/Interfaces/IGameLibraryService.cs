using fiap_fase1_tech_challenge.Models;

namespace fiap_fase1_tech_challenge.Services.Interfaces
{
    public interface IGameLibraryService
    {
        Task<IEnumerable<GameLibrary>> GetAllAsync();
        Task<GameLibrary?> GetByIdAsync(int id);
        Task<GameLibrary> CreateAsync(GameLibrary gameLibrary);
        Task<bool> UpdateAsync(GameLibrary gameLibrary);
        Task<bool> DeleteAsync(int id);
    }
}
