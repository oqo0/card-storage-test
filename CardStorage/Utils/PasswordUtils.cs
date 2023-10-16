using System;
using System.Security.Cryptography;
using System.Text;

namespace CardStorage.Utils;

public static class PasswordUtils
{
    public const string SecretKey = "Xf9zRvYrV4JGo56K5w5H6mpYvEeDs2wC1ynQW2DiLM8XXloSxi4JepQDkug0R2ET59e651LIfui2GcP";
    private const int HashStrength = 10;

    public static (string passwordHash, string passwordSalt) GeneratePasswordHash(string password)
    {
        var saltBytes = new byte[16];
        RandomNumberGenerator.Fill(saltBytes);

        string passwordSalt = Convert.ToBase64String(saltBytes);
        string passwordHash = GetPasswordHash(password, passwordSalt);

        return (passwordHash, passwordSalt);
    }

    public static bool CheckPassword(string password, string hash, string salt)
        => GetPasswordHash(password, salt) == hash;
    
    private static string GetPasswordHash(string password, string salt)
    {
        using var sha512 = SHA512.Create();
        
        string strToHash = password + salt + SecretKey;
        
        var passwordBytes = Encoding.UTF8.GetBytes(strToHash);

        for (int i = 0; i < HashStrength; i++)
        {
            passwordBytes = sha512.ComputeHash(passwordBytes);
        }

        return Convert.ToBase64String(passwordBytes);
    }
}

