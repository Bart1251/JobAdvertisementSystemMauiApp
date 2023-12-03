using System.Security.Cryptography;
using System.Text;

namespace JobAdvertisementApp.Services
{
    public class PasswordHasher
    {
        public string HashPassword(string password, string salt)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password + salt));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }

        public string GenerateSalt()
        {
            byte[] saltBytes = new byte[16];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(saltBytes);
            }
            return BitConverter.ToString(saltBytes).Replace("-", "").ToLower();
        }
    }
}
