using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Common.Misc;
using Common.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace ToDoService.Misc;

public class JwtTokenHelper
{
    private GeneralSettings settings;

    public JwtTokenHelper(IOptions<GeneralSettings> settings)
    {
        this.settings = settings.Value;
    }

    public string CreateToken(User user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.EncryptionKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var claims = user.ToClaims();

        var secToken = new JwtSecurityToken(
            "ToDoApi",  "ToDoApi", claims,
            expires: DateTime.Now.AddMinutes(120),
            signingCredentials: credentials);

        var token =  new JwtSecurityTokenHandler().WriteToken(secToken);
        return token;
    }
}