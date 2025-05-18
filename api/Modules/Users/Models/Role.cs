using fiap_fase1_tech_challenge.Common.Models;
using System.ComponentModel.DataAnnotations;

namespace fiap_fase1_tech_challenge.Modules.Users.Models
{
    public class Role:BaseModel
    {
        public string Name { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
