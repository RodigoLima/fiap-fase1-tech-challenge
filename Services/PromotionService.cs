using fiap_fase1_tech_challenge.Models;
using fiap_fase1_tech_challenge.Repositories.Interfaces;
using fiap_fase1_tech_challenge.Services.Interfaces;

namespace fiap_fase1_tech_challenge.Services
{
    public class PromotionService: IPromotionService
    {

        private readonly IPromotionRepository _promotionRepository;

        public PromotionService(IPromotionRepository promotionRepository)
        {
            _promotionRepository = promotionRepository;
        }

        public Task<IEnumerable<Promotion>> GetAllAsync() => _promotionRepository.GetAllAsync();
        public Task<Promotion?> GetByIdAsync(int id) => _promotionRepository.GetByIdAsync(id);
        public Task<Promotion> CreateAsync(Promotion promotion) => _promotionRepository.CreateAsync(promotion);
        public Task<bool> UpdateAsync(Promotion promotion) => _promotionRepository.UpdateAsync(promotion);
        public Task<bool> DeleteAsync(int id) => _promotionRepository.DeleteAsync(id);

    }
}
