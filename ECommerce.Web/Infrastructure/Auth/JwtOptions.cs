namespace ECommerce.Web.Infrastructure.Auth
{
    public class JwtOptions
    {
        public string SecretKey { get; set; } = string.Empty;
        public string CookieName { get; set; } = string.Empty;
        public string AccountIdClaimName { get; set; } = string.Empty;
        public int ExpiresHours { get; set; }
    }
}
