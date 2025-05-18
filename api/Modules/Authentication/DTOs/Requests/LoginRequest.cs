using System.ComponentModel;

namespace fiap_fase1_tech_challenge.Modules.Authentication.DTOs.Requests
{
    public class LoginRequest
    {
        [DefaultValue("adm@fcg.com")]
        public required string Email { get; set; }
        [DefaultValue("12345678")]
        public required string Password { get; set; }
    }
}

