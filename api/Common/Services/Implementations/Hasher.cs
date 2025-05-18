using fiap_fase1_tech_challenge.Common.Services.Interfaces;

namespace fiap_fase1_tech_challenge.Common.Services.Implementations
{
    public class Hasher : IHasher
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
