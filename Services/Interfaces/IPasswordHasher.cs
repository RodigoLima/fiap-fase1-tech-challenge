namespace fiap_fase1_tech_challenge.Services.Interfaces
{
    public interface IPasswordHasher
    {
        string Hash(string password);
        bool Verify(string plainText, string hash);
    }

}
