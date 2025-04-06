using System.ComponentModel.DataAnnotations;

namespace fiap_fase1_tech_challenge.Models
{
    public class BaseModel
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
    }
}
