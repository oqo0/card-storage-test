using CardStorage.Models;
using CardStorage.Models.Requests;
using CardStorage.Models.Responses;

namespace CardStorage.Services.Authentication;

public interface IAuthenticateService
{
    public AuthenticationResponse Login(AuthenticationRequest authenticationRequest);

    public SessionInfo? GetSessionInfo(string sessionToken);
}