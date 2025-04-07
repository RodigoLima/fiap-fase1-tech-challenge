using System.ComponentModel.DataAnnotations;

namespace fiap_fase1_tech_challenge.Models
{
    public class Game : BaseModel
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        [Required]
        public double Price { get; set; }
        public DateTime ReleasedDate { get; set; }
        public string Genre { get; set; } = string.Empty;

        public virtual ICollection<GameLibrary> GameLibrary { get; set; } = null!;

    }
}
