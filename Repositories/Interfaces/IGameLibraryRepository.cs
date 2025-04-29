using fiap_fase1_tech_challenge.Models;

namespace fiap_fase1_tech_challenge.Repositories.Interfaces
{
    public interface IGameLibraryRepository
    {
        Task<IEnumerable<GameLibrary>> GetAllByUser(int userId);
        Task<GameLibrary?> GetByIdAsync(int id);
        Task<GameLibrary> CreateAsync(GameLibrary gameLibrary);
        Task<bool> UpdateAsync(GameLibrary gameLibrary);
        Task<bool> DeleteAsync(int id);
    }
}
