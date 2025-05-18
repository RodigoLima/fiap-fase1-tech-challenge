using fiap_fase1_tech_challenge.Modules.Promotions.DTOs.Requests;
using fiap_fase1_tech_challenge.Modules.Promotions.DTOs.Responses;
using fiap_fase1_tech_challenge.Modules.Promotions.Models;

namespace fiap_fase1_tech_challenge.Modules.Promotions.Services.Interfaces
{
  public interface IPromotionService
  {
    Task<IEnumerable<Promotion>> GetAllAsync();
    Task<Promotion?> GetByIdAsync(int id);
    Task<PromotionResponse> CreateAsync(PromotionCreateRequest Promotion);
    Task<bool> UpdateAsync(int id,PromotionUpdateRequest Promotion);
    Task<bool> DeleteAsync(int id);
  }
}
