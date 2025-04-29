namespace fiap_fase1_tech_challenge.Models
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
