using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SeminarManager.API.Misc;
using SeminarManager.Model;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SeminarManager.API.Controllers
{
    public class AuthenticationController : Controller
    {
        private IConfiguration configuration;
        private IRepository repository;

        public AuthenticationController(IConfiguration configuration, IRepository repository)
        {
            this.configuration = configuration;
            this.repository = repository;
        }

        public IActionResult Index(string email, string password)
        {
            var user = repository.Persons.FindAdminByEmailAndPassword(email, password);
            if (user == null)
                return Json(new OperationResult("Username or password is incorrect"));

            var token = generateJwtToken(user);
            return Json(new { Token = token });
        }

        private string generateJwtToken(Person user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(configuration["key"]);
            var tokenDescriptor = new SecurityTokenDescriptor();
            tokenDescriptor.Subject = new ClaimsIdentity(new[] { new Claim("id", user.ID.ToString()) });
            tokenDescriptor.Expires = DateTime.UtcNow.AddDays(7);
            tokenDescriptor.SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
