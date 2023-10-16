namespace CardStorage.Models.Requests;

public class ChangeClientRequest
{
    public ulong UserId { get; set; }
    public string? FirstName { get; set; }
    
    public string? SureName { get; set; }
}