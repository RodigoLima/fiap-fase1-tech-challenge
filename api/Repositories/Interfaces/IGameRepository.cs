using fiap_fase1_tech_challenge.Models;

namespace fiap_fase1_tech_challenge.Repositories.Interfaces
{
    public interface IGameRepository
    {
        Task<IEnumerable<Game>> GetAllAsync();
        Task<Game?> GetByIdAsync(int id);
        Task<Game> CreateAsync(Game game);
        Task<bool> UpdateAsync(Game game);
        Task<bool> DeleteAsync(int id);
    }
}
