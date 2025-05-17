namespace fiap_fase1_tech_challenge.DTOs.Game
{
    public class GameUpdateRequest
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public double? Price { get; set; }
        public DateTime? ReleasedDate { get; set; }
        public string? Genre { get; set; }
    }
}
