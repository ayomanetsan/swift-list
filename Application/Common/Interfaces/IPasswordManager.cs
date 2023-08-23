namespace Application.Common.Interfaces
{
    public interface IPasswordManager
    {
        byte[] GenerateSalt();

        byte[] HashPassword(string password, byte[] salt);

        bool VerifyPassword(string password, string hash, string salt);
    }
}
