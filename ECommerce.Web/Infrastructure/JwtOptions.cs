namespace ECommerce.Web.Infrastructure
{
    public class JwtOptions
    {
        public string SecretKey { get; set; } = string.Empty;
        public string CookieName { get; set; } = string.Empty;
        public int ExpiresHours { get; set; }
    }
}
