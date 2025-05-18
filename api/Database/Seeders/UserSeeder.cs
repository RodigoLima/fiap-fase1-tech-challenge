using fiap_fase1_tech_challenge.Modules.Users.Models;

namespace fiap_fase1_tech_challenge.Database.Seeders
{
    public class UserSeeder: ISeeder
    {
        public void Seed(ApplicationContext context)
        {
            User inicialUser = new User { 
              Name= "administrador",
              Email = "adm@fcg.com",
              Password = "$2y$10$yAbAkW8OrbGo4PjJsMEPmeemtO1ugeX.47HFa99nqdu5H6gmm7OnK", //@AdminFCG1234
              RoleId = 1,
            };

            bool existingUsers = context.Users
                .Where(r => inicialUser.Name.Contains(r.Name))
                .Select(r => r.Name)
                .Any();
            if (!existingUsers)
            {
              context.Users.Add(inicialUser);
              context.SaveChanges();
            }
        }
    }
}
