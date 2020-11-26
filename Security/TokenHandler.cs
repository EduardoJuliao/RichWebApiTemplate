using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RichWebApiTemplate.Interfaces.Security;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;

namespace RichWebApiTemplate.Security
{
    public class TokenHandler : ITokenHandler
    {
        private readonly SecuritySettings _security;

        public TokenHandler(IOptions<SecuritySettings> securityOptions)
        {
            _security = securityOptions.Value;
        }

        public string GenerateToken()
        {
            var mySecret = _security.Secret;
            var mySecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(mySecret));

            var myIssuer = _security.Issuer;

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = myIssuer,
                SigningCredentials = new SigningCredentials(mySecurityKey, SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public bool ValidateCurrentToken(string token)
        {
            var mySecret = _security.Secret;
            var mySecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(mySecret));

            var issuer = _security.Issuer;
            var audiences = _security.Audiences;

            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = _security.Validations.ValidateIssuerSigningKey,
                    ValidateIssuer = _security.Validations.ValidateIssuer,
                    ValidIssuer = issuer,
                    ValidateAudience = audiences != null && audiences.Any(),
                    ValidAudiences = audiences,
                    IssuerSigningKey = mySecurityKey,
                    RequireExpirationTime = _security.Validations.ExpirationDate
                }, out SecurityToken validatedToken);
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}
