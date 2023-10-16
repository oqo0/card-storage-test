namespace CardStorage.Models.Responses;

public class GetCardByIdResponse : IOperationResult
{
    public CardDto Card { get; set; }
    public int ErrorCode { get; }
    public string? ErrorMessage { get; }
}