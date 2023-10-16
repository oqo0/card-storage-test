using System;
using System.Collections.Generic;
using System.Linq;
using CardStorage.Data;
using CardStorage.Data.Models;
using Microsoft.Extensions.Logging;

namespace CardStorage.Services.Repository.Impl;

public class AccountSessionRepository : IAccountSessionRepository
{
    #region Services

    private readonly CardStorageDbContext _context;
    private readonly ILogger<AccountSessionRepository> _logger;

    #endregion

    #region Constructors

    public AccountSessionRepository(
        CardStorageDbContext context,
        ILogger<AccountSessionRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    #endregion
    
    public AccountSession Create(AccountSession data)
    {
        _context.AccountSessions.Add(data);
        _context.SaveChanges();
        return data;
    }

    public AccountSession? GetById(long id)
    {
        return _context.AccountSessions.FirstOrDefault(a => a.Id == id);
    }

    public IList<AccountSession> GetAll()
    {
        return _context.AccountSessions.ToList();
    }
    
    public AccountSession? GetByToken(string token)
    {
        return _context.AccountSessions.FirstOrDefault(s => s.SessionToken == token);
    }
    
    public bool Update(AccountSession data)
    {
        throw new NotSupportedException();
    }

    public bool Delete(long id)
    {
        var accountSession = GetById(id);

        if (accountSession is null)
            return false;
        
        _context.Remove(accountSession);

        return _context.SaveChanges() != 0;
    }
}