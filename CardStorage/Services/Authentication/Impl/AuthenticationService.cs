using System;
using System.Collections.Generic;
using System.Security.Claims;
using CardStorage.Data.Models;
using CardStorage.Models;
using CardStorage.Models.Requests;
using CardStorage.Models.Responses;
using CardStorage.Services.Repository;
using CardStorage.Utils;
using Microsoft.Extensions.DependencyInjection;

namespace CardStorage.Services.Authentication.Impl;

public class AuthenticateService : IAuthenticateService
{
    #region Services

    private readonly IServiceScopeFactory _serviceScopeFactory;

    #endregion
    
    private readonly Dictionary<string, SessionInfo?> _sessions = new();

    public AuthenticateService(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }
    
    public AuthenticationResponse Login(AuthenticationRequest authenticationRequest)
    {
        using var scope = _serviceScopeFactory.CreateScope();
        var accountRepository = scope.ServiceProvider.GetRequiredService<IAccountRepository>();
        var sessionRepository = scope.ServiceProvider.GetRequiredService<IAccountSessionRepository>();

        var account = accountRepository.FindAccountByLogin(authenticationRequest.Login);

        if (account is null)
            return new AuthenticationResponse
            {
                Status = AuthenticationStatus.UserNotFound
            };

        bool passwordCorrect = PasswordUtils.CheckPassword(
            authenticationRequest.Password, account.PasswordHash, account.PasswordSalt);
        
        if (!passwordCorrect)
            return new AuthenticationResponse
            {
                Status = AuthenticationStatus.InvalidPassword
            };

        string sessionToken = GetSessionToken(account.Id, account.Email);

        var accountSession = new AccountSession
        {
            Account = account,
            AccountId = account.Id,
            IsClosed = false,
            SessionToken = sessionToken,
            TimeCreated = DateTime.Now
        };

        sessionRepository.Create(accountSession);

        var sessionInfo = GetSessionInfo(account, accountSession);

        lock (_sessions)
            _sessions.Add(sessionInfo.Token, sessionInfo);

        return new AuthenticationResponse
        {
            Status = AuthenticationStatus.Success,
            SessionInfo = sessionInfo
        };
    }
    
    public SessionInfo? GetSessionInfo(string sessionToken)
    {
        SessionInfo? sessionInfo;

        lock (_sessions)
            _sessions.TryGetValue(sessionToken, out sessionInfo);

        if (sessionInfo is not null)
            return sessionInfo;
        
        using var scope = _serviceScopeFactory.CreateScope();
        var accountRepository = scope.ServiceProvider.GetRequiredService<IAccountRepository>();
        var sessionRepository = scope.ServiceProvider.GetRequiredService<IAccountSessionRepository>();

        var accountSession = sessionRepository.GetByToken(sessionToken);
        
        if (accountSession is null)
            return null;
        
        var account = accountRepository.GetById(accountSession.Id);
        
        if (account is null)
            return null;
        
        sessionInfo = GetSessionInfo(account, accountSession);

        lock (_sessions)
            _sessions.Add(sessionInfo.Token, sessionInfo);
        
        return sessionInfo;
    }

    #region Private methods

    private static string GetSessionToken(long id, string email)
    {
        var accountClaims = new Claim[]
        {
            new(ClaimTypes.NameIdentifier, id.ToString()),
            new(ClaimTypes.Email, email)
        };

        return SessionUtils.CreateSessionToken(accountClaims);
    }

    private static SessionInfo GetSessionInfo(Account account, AccountSession accountSession)
    {
        return new SessionInfo
        {
            Id = accountSession.Id,
            AccountDto = new AccountDto
            {
                Id = account.Id,
                Email = account.Email,
                FirstName = account.FirstName,
                LastName = account.LastName
            },
            Token = accountSession.SessionToken
        };
    }

    #endregion
}