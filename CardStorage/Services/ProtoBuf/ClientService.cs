using CardStorage.Data.Models;
using CardStorage.Services.Repository;
using Grpc.Core;
using static CardStorageServiceProtos.ClientService;
using CreateClientRequest = CardStorageServiceProtos.CreateClientRequest;
using CreateClientResponse = CardStorageServiceProtos.CreateClientResponse;

namespace CardStorage.Services.ProtoBuf;

public class ClientService : ClientServiceBase
{
    #region Services

    private readonly IUserRepository _userRepository;
    
    #endregion

    public ClientService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    public override Task<CreateClientResponse> Create(
        CreateClientRequest request,
        ServerCallContext context)
    {
        var user = _userRepository.Create(new User()
        {
            FirstName = request.FirstName,
            SureName = request.SureName
        });

        var response = new CreateClientResponse
        {
            ClientId = user.Id
        };
        
        return Task.FromResult(response);
    }
}