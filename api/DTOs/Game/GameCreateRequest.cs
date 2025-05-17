using System.ComponentModel.DataAnnotations;

namespace fiap_fase1_tech_challenge.DTOs.Game
{
  public class GameCreateRequest
  {
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
    public double Price { get; set; }
    public DateTime? ReleasedDate { get; set; }
    public string Genre { get; set; } = string.Empty;
  }
}