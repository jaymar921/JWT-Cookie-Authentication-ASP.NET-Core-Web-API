namespace JWT_Cookie_Authentication_ASP.NET_Core_Web_API.Helpers
{
    public class AppSettings
    {
        public string SecretKey { get; set; } = string.Empty;
        public int IntExpireHour { get; set; } = 0;
    }
}
