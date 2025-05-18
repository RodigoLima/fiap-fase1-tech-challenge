using fiap_fase1_tech_challenge.Modules.Promotions.Models;

namespace fiap_fase1_tech_challenge.Modules.Promotions.Repositories.Interfaces
{
    public interface IPromotionRepository
    {
        Task<IEnumerable<Promotion>> GetAllAsync();
        Task<Promotion?> GetByIdAsync(int id);
        Task<IEnumerable<Promotion>> GetByGame(int gameId);
        Task<Promotion> CreateAsync(Promotion promotion);
        Task<bool> UpdateAsync(Promotion promotion);
        Task<bool> DeleteAsync(int id);
    }
}
