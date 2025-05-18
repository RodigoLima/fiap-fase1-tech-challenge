using fiap_fase1_tech_challenge.Common.Models;
using fiap_fase1_tech_challenge.Modules.GamesLibrary.Models;
using fiap_fase1_tech_challenge.Modules.Promotions.Models;

namespace fiap_fase1_tech_challenge.Modules.Games.Models
{
    public class Game : BaseModel
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        public double Price { get; set; }
        public DateTime? ReleasedDate { get; set; }
        public required string Genre { get; set; }

        public virtual ICollection<GameLibrary> GameLibraries { get; set; }

        public virtual ICollection<Promotion> Promotions { get; set; }

    }
}
