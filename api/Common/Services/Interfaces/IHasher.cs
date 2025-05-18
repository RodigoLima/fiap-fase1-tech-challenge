namespace fiap_fase1_tech_challenge.Common.Services.Interfaces
{
    public interface IHasher
    {
        string Hash(string password);
        bool Verify(string plainText, string hash);
    }

}
