using System.ComponentModel.DataAnnotations;

namespace fiap_fase1_tech_challenge.Models
{
    public class User : BaseModel
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;

        [Required]
        public int RoleId { get; set; }

        public virtual Role Role { get; set; } = null!;
        public virtual ICollection<GameLibrary> GameLibrary { get; set; } = new List<GameLibrary>();
    }

}
