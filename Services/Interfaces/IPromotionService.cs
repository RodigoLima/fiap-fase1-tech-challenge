using fiap_fase1_tech_challenge.DTOs.Promotion;
using fiap_fase1_tech_challenge.Models;

namespace fiap_fase1_tech_challenge.Services.Interfaces
{
  public interface IPromotionService
  {
    Task<IEnumerable<Promotion>> GetAllAsync();
    Task<Promotion?> GetByIdAsync(int id);
    Task<PromotionResponse> CreateAsync(PromotionCreateRequest Promotion);
    Task<bool> UpdateAsync(Promotion Promotion);
    Task<bool> DeleteAsync(int id);
  }
}
