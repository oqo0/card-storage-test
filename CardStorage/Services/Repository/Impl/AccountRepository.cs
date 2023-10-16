using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using CardStorage.Data;
using CardStorage.Data.Models;
using Microsoft.Extensions.Logging;

namespace CardStorage.Services.Repository.Impl;

public class AccountRepository : IAccountRepository
{
    #region Services

    private readonly CardStorageDbContext _context;
    private readonly ILogger<AccountRepository> _logger;

    #endregion

    #region Constructors

    public AccountRepository(
        CardStorageDbContext context,
        ILogger<AccountRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    #endregion
    
    public Account Create(Account data)
    {
        throw new NotImplementedException();
    }

    public Account? GetById(long id)
    {
        throw new NotImplementedException();
    }

    public IList<Account> GetAll()
    {
        throw new NotImplementedException();
    }

    public Account? FindAccountByLogin(string login)
    {
        return _context.Accounts.FirstOrDefault(a => a.Email == login);
    }
    
    public bool Update(Account data)
    {
        throw new NotImplementedException();
    }

    public bool Delete(long id)
    {
        throw new NotImplementedException();
    }
}