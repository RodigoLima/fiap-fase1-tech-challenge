using fiap_fase1_tech_challenge.Models;

namespace fiap_fase1_tech_challenge.Services.Interfaces
{
    public interface IGameService
    {
        Task<IEnumerable<Game>> GetAllAsync();
        Task<Game?> GetByIdAsync(int id);
        Task<Game> CreateAsync(Game gameLibrary);
        Task<bool> UpdateAsync(Game gameLibrary);
        Task<bool> DeleteAsync(int id);
    }
}
