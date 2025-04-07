using System.ComponentModel.DataAnnotations;

namespace fiap_fase1_tech_challenge.Models
{
    public class Promotion : BaseModel
    {
        [Required]
        public int DiscountPercent { get; set; } 
        [Required]
        public DateTime InitialDate { get; set; }
        [Required]
        public DateTime FinalDate { get; set; }
        [Required]
        public int GameId { get; set; }

        public virtual Game Game { get; set; } = null!;
    }
}
