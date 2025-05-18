namespace fiap_fase1_tech_challenge.Modules.Users.DTOs.Responses
{
    public class UserResponse
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string RoleName { get; set; } = string.Empty;
    }
}
