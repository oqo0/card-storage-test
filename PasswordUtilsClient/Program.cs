using CardStorage.Utils;

string userPassword = Console.ReadLine() ?? String.Empty;

var hash = PasswordUtils.GeneratePasswordHash(userPassword);

Console.WriteLine("-------------------------------------------");
Console.WriteLine("- Password Hash ---------------------------");
Console.WriteLine($"- {hash.passwordHash} -");
Console.WriteLine("- Password Salt ---------------------------");
Console.WriteLine($"- {hash.passwordSalt} -");
Console.WriteLine("-------------------------------------------");

Console.WriteLine();
Console.WriteLine("Enter user password: ");

userPassword = Console.ReadLine() ?? string.Empty;

bool isPasswordCorrect = PasswordUtils.CheckPassword(userPassword, hash.passwordHash, hash.passwordSalt);

Console.WriteLine(isPasswordCorrect);