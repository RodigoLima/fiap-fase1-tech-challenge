using System.ComponentModel.DataAnnotations;

namespace fiap_fase1_tech_challenge.Models
{
    public class GameLibrary : BaseModel
    {
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public Guid GameId { get; set; }
        [Required]
        public DateTime PurchaseDate { get; set; }

        public virtual User User { get; set; }
        public virtual Game Game { get; set; }
    }
}
