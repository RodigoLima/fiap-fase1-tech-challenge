public class PromotionUpdateRequest
{
    public required int DiscountPercentage { get; set; }
    public required DateTime InitialDate { get; set; }
    public required DateTime FinalDate { get; set; }
    public required int GameId { get; set; }
}
