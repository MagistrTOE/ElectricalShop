namespace Core.Configuration
{
    public class IdentitySettings
    {
        public string AuthorityUrl { get; set; }
        public string Audience { get; set; }
        public TimeSpan TokenLifeTime { get; set; }
    }
}
