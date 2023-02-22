using System.Security.Claims;
using System.Text;
using mini_project_data.Entities;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace mini_project_business.Utilities;

public interface IJwtHelper
{
    string GenerateJwtToken(User user, Role role, int id);
}
public class JwtHelper : IJwtHelper
{
    private readonly IConfiguration _config;

    public JwtHelper(IConfiguration config)
    {
        _config = config;
    }

    public string GenerateJwtToken(User user, Role role, int id)
    {
        // security key
        string? securityKey = _config["JWT:Key"];

        // symmetric security key
        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey!));

        // signing credentials
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);

        var claim = new[]{
            new Claim("Id", id.ToString()),
            new Claim("Phone", user.Phone),
            new Claim("Name", user.Name),
            new Claim(ClaimTypes.Role, role.Name)
        };

        // create token
        var token = new JwtSecurityToken(
            issuer: _config["JWT:Issuer"],
            audience: _config["JWT:Audience"],
            notBefore: DateTime.Now,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: signingCredentials,
            claims: claim
        );

        var tokenHandler = new JwtSecurityTokenHandler();
            
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claim),
            Expires = DateTime.Now.AddDays(1),
            SigningCredentials =  signingCredentials};
        var tokens = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);
        var jwtToken = tokenHandler.WriteToken(tokens);
            
        // return token
        return jwtToken;
    }
}