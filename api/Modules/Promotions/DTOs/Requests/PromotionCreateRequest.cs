using System.ComponentModel.DataAnnotations;

namespace fiap_fase1_tech_challenge.Modules.Promotions.DTOs.Requests
{
  public class PromotionCreateRequest
  {
    public int DiscountPercentage { get; set; }
    public DateTime InitialDate { get; set; }
    public DateTime FinalDate { get; set; }
    [Required]
    public int GameId { get; set; }
  }
}