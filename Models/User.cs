namespace fiap_fase1_tech_challenge.Models
{
    public class User : BaseModel
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public int RoleId { get; set; }

        public virtual Role Role { get; set; }
        public virtual ICollection<GameLibrary> GameLibraries { get; set; }
    }

}
