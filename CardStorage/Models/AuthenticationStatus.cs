namespace CardStorage.Models;

public enum AuthenticationStatus
{
    Success = 0,
    UserNotFound = 1,
    InvalidPassword = 2,
    InvalidInputData = 3
}