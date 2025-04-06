using System.ComponentModel.DataAnnotations;

namespace fiap_fase1_tech_challenge.Models
{
    public class Game : BaseModel
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public double Price { get; set; }
        public DateTime ReleasedDate { get; set; }
        public string Genre { get; set; }

        public virtual ICollection<GameLibrary> GameLibrary { get; set; }

    }
}
