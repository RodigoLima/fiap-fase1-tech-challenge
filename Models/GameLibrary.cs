namespace fiap_fase1_tech_challenge.Models
{
    public class GameLibrary : BaseModel
    {
        public int UserId { get; set; }
        public int GameId { get; set; }

        public virtual User User { get; set; }
        public virtual Game Game { get; set; }
    }
}
