using Microsoft.AspNetCore.Http;
using RichWebApiTemplate.Interfaces.Security;
using System;
using System.Linq;
using System.Threading.Tasks;


namespace RichWebApiTemplate.Middlewares
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ITokenHandler _tokenHandler;

        public JwtMiddleware(RequestDelegate next, ITokenHandler tokenHandler)
        {
            _next = next;
            _tokenHandler = tokenHandler;
        }

        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (_tokenHandler.ValidateCurrentToken(token))
                await _next(context);
            else
                throw new UnauthorizedAccessException("Invalid token");
        }
    }
}
