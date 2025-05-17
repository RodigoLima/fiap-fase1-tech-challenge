using fiap_fase1_tech_challenge.Models;

namespace fiap_fase1_tech_challenge.Database.Seeders
{
    public class UserSeeder: ISeeder
    {
        public void Seed(ApplicationContext context)
        {
            User inicialUser = new User { 
              Name= "administrador",
              Email = "adm@fcg.com",
              Password = "$2a$11$R7ZmOsHqdsMR8i2pSr8sceWpXQVnGqd52JCc7CcsLeg2.L5RFDAfi",// '12345678
              RoleId = 1,
            };

            bool existingRoles = context.Roles
                .Where(r => inicialUser.Name.Contains(r.Name))
                .Select(r => r.Name)
                .Any();
            if (!existingRoles)
            {
              context.Users.Add(inicialUser);
              context.SaveChanges();
            }
        }
    }
}
