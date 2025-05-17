namespace fiap_fase1_tech_challenge.Models
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
