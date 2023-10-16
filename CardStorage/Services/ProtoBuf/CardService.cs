using System;
using System.Linq;
using System.Threading.Tasks;
using CardStorage.Services.Repository;
using CardStorageServiceProtos;
using Grpc.Core;
using static CardStorageServiceProtos.CardService;
using Card = CardStorageServiceProtos.Card;

namespace CardStorage.Services.ProtoBuf;

public class CardService : CardServiceBase
{
    #region Services

    private readonly IUserRepository _userRepository;
    private readonly ICardRepository _cardRepository;
    
    #endregion
    
    public CardService(IUserRepository userRepository, ICardRepository cardRepository)
    {
        _userRepository = userRepository;
        _cardRepository = cardRepository;
    }

    public override Task<GetByClientIdResponse> GetByClientId(GetByClientIdRequest request, ServerCallContext context)
    {
        var response = new GetByClientIdResponse();

        response.Cards.AddRange(
            _cardRepository.GetAllByUser(request.ClientId)
                .Select(x => 
                    new Card()
                    {
                        CVV2 = Int32.Parse(x.CVV2),
                        ExpDate = x.ExpDateTime.ToString(),
                        CardId = long.Parse(x.Number)
                    }));

        return Task.FromResult(response);
    }
}