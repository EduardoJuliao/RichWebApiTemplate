namespace RichWebApiTemplate.Security
{
    public class SecuritySettings
    {
        public string Issuer { get; set; }
        public string Secret { get; set; }
        public string[] Audiences { get; set; }
        public Validations Validations { get; set; }
    }

    public class Validations
    {
        public bool ExpirationDate { get; set; }
        public bool ValidateIssuerSigningKey { get; set; }
        public bool ValidateIssuer { get; set; }
    }
}
