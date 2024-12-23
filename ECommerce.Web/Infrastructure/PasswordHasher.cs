using BCrypt.Net;
using ECommerce.Application.Interfaces;

namespace ECommerce.Web.Infrastructure
{
    public class PasswordHasher : IPasswordHasher
    {
        public string Hash(string password)
        {
            return BCrypt.Net.BCrypt.EnhancedHashPassword(password);
        }

        public bool Verify(string password, string hash)
        {
            return BCrypt.Net.BCrypt.EnhancedVerify(password, hash);
        }
    }
}
