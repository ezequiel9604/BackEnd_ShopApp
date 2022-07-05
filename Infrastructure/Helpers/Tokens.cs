
using Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Helpers;

public class Tokens
{

    public static string CreateToken(Client client, IConfiguration config)
    {
        List<Claim> claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, client.FirstName),
            new Claim(ClaimTypes.Surname, client.LastName),
            new Claim(ClaimTypes.Email, client.Email),
            new Claim(ClaimTypes.Role, "Client"),
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
            config.GetSection("AppSettings:TokenKey").Value
        ));

        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var jwt = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddHours(2),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(jwt);

    }

}
