using Application.Common.Interfaces;
using System.Security.Cryptography;

namespace Infrastructure.Identity
{
    public class PasswordManager : IPasswordManager
    {
        public byte[] GenerateSalt()
        {
            using (var rng = RandomNumberGenerator.Create())
            {
                byte[] salt = new byte[16];
                rng.GetBytes(salt);
                return salt;
            }
        }

        public byte[] HashPassword(string password, byte[] salt)
        {
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000, HashAlgorithmName.SHA256))
            {
                return pbkdf2.GetBytes(32);
            }
        }

        public void StorePassword(string password)
        {
            throw new NotImplementedException();
        }

        public bool VerifyPassword(string password, string hash, string salt)
        {
            byte[] hashBytes = Convert.FromBase64String(hash);
            byte[] saltBytes = Convert.FromBase64String(salt);

            byte[] providedHash = HashPassword(password, saltBytes);

            return SlowEquals(hashBytes, providedHash);
        }

        private static bool SlowEquals(byte[] a, byte[] b)
        {
            uint diff = (uint)a.Length ^ (uint)b.Length;
            for (int i = 0; i < a.Length && i < b.Length; i++)
                diff |= (uint)(a[i] ^ b[i]);
            return diff == 0;
        }
    }
}
