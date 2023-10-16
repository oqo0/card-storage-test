namespace CardStorage.Models;

public class SessionInfo
{
    public long Id { get; set; }
    public string Token { get; set; }
    public AccountDto AccountDto { get; set; }
}