using System;

namespace CardStorage.Models.Responses;

public class CreateCardResponse : IOperationResult
{
    public Guid Id { get; set; }
    
    public int ErrorCode { get; set; }
    
    public string? ErrorMessage { get; set; }
}