namespace CardStorage.Models.Responses;

public class CreateClientResponse : IOperationResult
{
    public ulong ClientId { get; set; }
    public int ErrorCode { get; set; }
    public string? ErrorMessage { get; set; }
}