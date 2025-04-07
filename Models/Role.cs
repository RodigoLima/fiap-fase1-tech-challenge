using System.ComponentModel.DataAnnotations;

namespace fiap_fase1_tech_challenge.Models
{
    public class Role:BaseModel
    {
        [Required]
        [MaxLength(5)]
        public string Name { get; set; } = string.Empty;
        public ICollection<User> Users { get; set; } = null!;
    }
}
