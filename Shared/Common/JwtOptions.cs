namespace Shared.Common
{
    public class JwtOptions
    {
        public string Issuer { get; set; } = string.Empty;
        public string Audience { get; set; }= string.Empty;
        public string SecretKey { get; set; }= string.Empty;
        public double ExpirationInDays { get; set; }

    }
}
