using System.ComponentModel.DataAnnotations;

namespace fiap_fase1_tech_challenge.Models
{
    public class User : BaseModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public Guid RoleId { get; set; }
        
        public virtual Role Role { get; set; } = null!;
        public virtual ICollection<GameLibrary> GameLibrary { get; set; }
    }
}
