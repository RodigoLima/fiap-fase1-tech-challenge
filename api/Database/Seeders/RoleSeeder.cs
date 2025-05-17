using fiap_fase1_tech_challenge.Models;

namespace fiap_fase1_tech_challenge.Database.Seeders
{
    public class RoleSeeder: ISeeder
    {
        public void Seed(ApplicationContext context)
        {
            var requiredRoles = new[] { "Admin", "User"};

            var existingRoles = context.Roles
                .Where(r => requiredRoles.Contains(r.Name))
                .Select(r => r.Name)
                .ToHashSet();

            var rolesToAdd = requiredRoles
                .Where(r => !existingRoles.Contains(r))
                .Select(name => new Role { Name = name });

            context.Roles.AddRange(rolesToAdd);
            context.SaveChanges();
        }
    }
}
