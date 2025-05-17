using System.ComponentModel.DataAnnotations;

namespace fiap_fase1_tech_challenge.DTOs.Promotion
{
  public class PromotionCreateRequest
  {
    [Required, Range(1,100, ErrorMessage = "O desconto deve ser de 1% à 100%.")]
    public int DiscountPercentage { get; set; }
    [Required]
    public DateTime InitialDate { get; set; }
    [Required]
    public DateTime FinalDate { get; set; }
    [Required]
    public int GameId { get; set; }
  }
}