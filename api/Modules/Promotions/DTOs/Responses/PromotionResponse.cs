namespace fiap_fase1_tech_challenge.Modules.Promotions.DTOs.Responses
{
  public class PromotionResponse
  {
    public int Id { get; set; }
    public int DiscountPercentage { get; set; }
    public DateTime InitialDate { get; set; }
    public DateTime FinalDate { get; set; }
    public int GameId { get; set; }
  }
}
