using fiap_fase1_tech_challenge.DTOs.Promotion;
using fiap_fase1_tech_challenge.Models;

namespace fiap_fase1_tech_challenge.Services.Interfaces
{
    public interface IPromotionService
    {
        Task<IEnumerable<Promotion>> GetAllAsync();
        Task<Promotion?> GetByIdAsync(int id);
        Task<Promotion> CreateAsync(PromotionCreateRequest promotion);
        Task<bool> UpdateAsync(int id,PromotionUpdateRequest promotion);
        Task<bool> DeleteAsync(int id);
    }
}
