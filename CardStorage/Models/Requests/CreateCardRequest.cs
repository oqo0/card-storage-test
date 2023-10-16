using System;

namespace CardStorage.Models.Requests;

public class CreateCardRequest
{
    public ulong OwnerId { get; set; }
    
    public string Number { get; set; }
    
    public string OwnerName { get; set; }
    
    public string CVV2 { get; set; }
    
    public DateTime ExpDateTime { get; set; }
}