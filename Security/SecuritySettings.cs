namespace RichWebApiTemplate.Security
{
    public class SecuritySettings
    {
        public string Issuer { get; set; }
        public string Secret { get; set; }
        public string[] Audiences { get; set; }
    }
}
