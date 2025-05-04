using fiap_fase1_tech_challenge.DTOs.Promotion;
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
        public Task<Promotion> CreateAsync(PromotionCreateRequest promotion)
        {
            var newPromotion = new Promotion
            {
                GameId = promotion.GameId,
                DiscountPercentage = promotion.DiscountPercentage,
                InitialDate = promotion.InitialDate,
                FinalDate = promotion.FinalDate
            };
            return _promotionRepository.CreateAsync(newPromotion);
        }
        public async Task<bool> UpdateAsync(int id, PromotionUpdateRequest promotion)
        {
            var newPromotion = await _promotionRepository.GetByIdAsync(id);
            if (newPromotion == null)
                return false;

            newPromotion.GameId = promotion.GameId;
            newPromotion.DiscountPercentage = promotion.DiscountPercentage;
            newPromotion.InitialDate = promotion.InitialDate;
            newPromotion.FinalDate = promotion.FinalDate;

            await _promotionRepository.UpdateAsync(newPromotion);

            return true;
        }
        public Task<bool> DeleteAsync(int id) => _promotionRepository.DeleteAsync(id);

    }
}
