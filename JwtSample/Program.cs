using System;
using JwtSample;

string userName = Console.ReadLine()!;
string password = Console.ReadLine()!;

var userService = new UserService();

Console.WriteLine(userService.Authenticate(userName, password));