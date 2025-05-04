namespace fiap_fase1_tech_challenge.DTOs.Promotion
{
    public class PromotionCreateRequest
    {
        public required int DiscountPercentage { get; set; }
        public required DateTime InitialDate { get; set; }
        public required DateTime FinalDate { get; set; }
        public required int GameId { get; set; }
    }
}
