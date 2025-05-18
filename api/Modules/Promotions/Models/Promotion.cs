using fiap_fase1_tech_challenge.Common.Models;
using fiap_fase1_tech_challenge.Modules.Games.Models;

namespace fiap_fase1_tech_challenge.Modules.Promotions.Models
{
    public class Promotion : BaseModel
    {
        public int DiscountPercentage { get; set; } 
        public DateTime InitialDate { get; set; }
        public DateTime FinalDate { get; set; }
        public int GameId { get; set; }

        public virtual Game Game { get; set; }
    }
}
