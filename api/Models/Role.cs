using System.ComponentModel.DataAnnotations;

namespace fiap_fase1_tech_challenge.Models
{
    public class Role:BaseModel
    {
        public string Name { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
