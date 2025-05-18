using fiap_fase1_tech_challenge.Common.Models;
using fiap_fase1_tech_challenge.Modules.Games.Models;
using fiap_fase1_tech_challenge.Modules.Users.Models;

namespace fiap_fase1_tech_challenge.Modules.GamesLibrary.Models
{
    public class GameLibrary : BaseModel
    {
        public int UserId { get; set; }
        public int GameId { get; set; }

        public virtual User User { get; set; }
        public virtual Game Game { get; set; }
    }
}
