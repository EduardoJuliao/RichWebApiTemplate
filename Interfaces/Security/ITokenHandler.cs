namespace RichWebApiTemplate.Interfaces.Security
{
    public interface ITokenHandler
    {
        string GenerateToken();
        bool ValidateCurrentToken(string token);
    }
}
