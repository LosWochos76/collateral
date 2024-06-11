using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Common.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace ToDoService.Misc;

public class JwtTokenHelper
{
    private JwtInfo jwtInfo;

    public JwtTokenHelper(IOptions<JwtInfo> jwtInfo)
    {
        this.jwtInfo = jwtInfo.Value;
    }

    public string CreateToken(User user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtInfo.Key));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var claims = user.ToClaims();

        var secToken = new JwtSecurityToken(
            jwtInfo.Issuer,  jwtInfo.Issuer, claims,
            expires: DateTime.Now.AddMinutes(120),
            signingCredentials: credentials);

        var token =  new JwtSecurityTokenHandler().WriteToken(secToken);
        return token;
    }
}