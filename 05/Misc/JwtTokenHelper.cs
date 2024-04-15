using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

public class JwtTokenHelper
{
    private IConfiguration config;

    public JwtTokenHelper(IConfiguration config)
    {
        this.config = config;
    }

    public string CreateToken(User user)
    {
        var jwtKey = config.GetValue<string>("Jwt:Key");
        var jwtIssuer = config.GetValue<string>("Jwt:Issuer");

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>() { new Claim("id", user.ID.ToString()) };
        if (user.IsAdmin)
            claims.Add(new Claim(ClaimTypes.Role, "Admin"));

        var Sectoken = new JwtSecurityToken(
            jwtIssuer, 
            jwtIssuer,
            claims,
            expires: DateTime.Now.AddMinutes(120),
            signingCredentials: credentials);

        var token =  new JwtSecurityTokenHandler().WriteToken(Sectoken);
        return token;
    }
}