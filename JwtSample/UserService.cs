using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace JwtSample;

internal class UserService
{
    private const string SecretCode = "Xf9zRvYrV4JGo56K5w5H6mpYvEeDs2wC1ynQW2DiLM8XXloSxi4JepQDkug0R2ET59e651LIfui2GcP";
    
    private readonly IDictionary<string, string> _users = new Dictionary<string, string>
    {
        { "oqo0", "123456" },
        { "root", "qwerty" },
        { "sudo", "password" },
        { "123", "123" },
    };

    public string? Authenticate(string username, string password)
    {
        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            return null;

        if (!_users.TryGetValue(username, out string? realPassword))
            return null;

        if (string.CompareOrdinal(password, realPassword) != 0)
            return null;

        return GenerateJwt(username);
    }

    private string GenerateJwt(string userName)
    {
        var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretCode));

        var securityTokenDescriptor = new SecurityTokenDescriptor
        {
            Expires = DateTime.UtcNow.AddMinutes(10),
            SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512),
            Subject = new ClaimsIdentity(new Claim[]
            {
                new(ClaimTypes.Name, userName)
            })
        };

        var securityToken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);

        return jwtSecurityTokenHandler.WriteToken(securityToken);
    }
}