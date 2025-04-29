using System.ComponentModel.DataAnnotations;

namespace fiap_fase1_tech_challenge.DTOs.Game
{
  public class GameCreateRequest
  {
    [Required, MaxLength(50)]
    public string Name { get; set; } = string.Empty;
    [MaxLength(200)]
    public string? Description { get; set; } = string.Empty;
    [Required]
    public double Price { get; set; }
    public DateTime? ReleasedDate { get; set; }
    [Required]
    public string Genre { get; set; } = string.Empty;
    [Required]
    public int RoleId { get; set; }
  }
}