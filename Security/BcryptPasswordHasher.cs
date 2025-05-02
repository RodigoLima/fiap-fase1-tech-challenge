using fiap_fase1_tech_challenge.Services.Interfaces;

namespace fiap_fase1_tech_challenge.Security
{
    public class BcryptPasswordHasher : IPasswordHasher
    {
        public string Hash(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool Verify(string plainText, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(plainText, hash);
        }
    }

}
