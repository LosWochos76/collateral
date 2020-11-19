using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SeminarManager.Model;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeminarManager.API.Misc
{
    public class CustomAuthorizationMiddleware
    {
        private RequestDelegate next;
        private IConfiguration configurtion;
        private IPersonRepository repository;

        public CustomAuthorizationMiddleware(RequestDelegate next, IConfiguration configurtion, IPersonRepository repository)
        {
            this.next = next;
            this.configurtion = configurtion;
            this.repository = repository;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Path.StartsWithSegments("/Authentication"))
            {
                var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

                if (token == null)
                {
                    SendError(context, "Unauthorized");
                    return;
                }

                var user = GetUserFromToken(token);
                if (user == null)
                {
                    SendError(context, "User not found");
                    return;
                }

                context.Items["user"] = user;
            }

            await next(context);
        }

        private void SendError(HttpContext context, string message)
        {
            context.Response.StatusCode = 500;
            context.Response.ContentType = "application/json";
            string json = System.Text.Json.JsonSerializer.Serialize(new OperationResult(message));
            context.Response.WriteAsync(json, Encoding.UTF8);
        }

        private Person GetUserFromToken(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(configurtion["key"]);

                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                int user_id = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);
                var user = repository.ById(user_id);
                return user;
            }
            catch
            {
                return null;
            }
        }
    }
}
