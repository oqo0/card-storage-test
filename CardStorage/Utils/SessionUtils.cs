using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace CardStorage.Utils;

public static class SessionUtils
{
    private const string SecretKey = "Xf9zRvYrV4JGo56K5w5H6mpYvEeDs2wC1ynQW2DiLM8XXloSxi4JepQDkug0R2ET59e651LIfui2GcP";
    
    public static string CreateSessionToken(Claim[] claims)
    {
        var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey));

        var securityTokenDescriptor = new SecurityTokenDescriptor
        {
            Expires = DateTime.UtcNow.AddMinutes(15),
            SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512),
            Subject = new ClaimsIdentity(claims)
        };

        var securityToken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);

        return jwtSecurityTokenHandler.WriteToken(securityToken);
    }
}