using System.Security.Cryptography;

namespace PerformanceAssessmentApi.Utils
{
    public static class PasswordHasher
    {
        private const int SaltSize = 32;
        private const int KeySize = 32;

        public static (string Hash, string Salt) HashPassword(string password)
        {
            using (var algorithm = new RNGCryptoServiceProvider())
            {
                var saltBytes = new byte[SaltSize];
                algorithm.GetBytes(saltBytes);

                using (var pbkdf2 = new Rfc2898DeriveBytes(password, saltBytes, 10000))
                {
                    var keyBytes = pbkdf2.GetBytes(KeySize);

                    var hashBytes = new byte[SaltSize + KeySize];
                    Array.Copy(saltBytes, 0, hashBytes, 0, SaltSize);
                    Array.Copy(keyBytes, 0, hashBytes, SaltSize, KeySize);

                    var hash = Convert.ToBase64String(hashBytes);
                    var salt = Convert.ToBase64String(saltBytes);

                    return (hash, salt);
                }
            }
        }

        public static bool VerifyPassword(string password, string hash, string salt)
        {
            var saltBytes = Convert.FromBase64String(salt);
            var hashBytes = Convert.FromBase64String(hash);

            using (var pbkdf2 = new Rfc2898DeriveBytes(password, saltBytes, 10000))
            {
                var keyBytes = pbkdf2.GetBytes(KeySize);

                for (var i = 0; i < KeySize; i++)
                {
                    if (keyBytes[i] != hashBytes[i + SaltSize])
                    {
                        return false;
                    }
                }
                return true;
            }
        }
    }
}