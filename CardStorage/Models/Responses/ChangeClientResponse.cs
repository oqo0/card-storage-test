namespace CardStorage.Models.Responses;

public class ChangeClientResponse : IOperationResult
{
    public ulong UserId { get; set; }
    public bool Status { get; set; }
    public int ErrorCode { get; }
    public string? ErrorMessage { get; }
}