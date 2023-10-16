using System.Collections.Generic;
using System.Linq;
using CardStorage.Data;
using CardStorage.Data.Models;
using Microsoft.Extensions.Logging;

namespace CardStorage.Services.Repository.Impl;

public class UserRepository : IUserRepository
{
    #region Services

    private readonly CardStorageDbContext _context;
    private readonly ILogger<UserRepository> _logger;

    #endregion

    #region Constructors

    public UserRepository(
        CardStorageDbContext context,
        ILogger<UserRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    #endregion

    public User Create(User data)
    {
        _context.Users.Add(data);
        _context.SaveChanges();
        return data;
    }

    public User? GetById(ulong id)
    {
        return _context.Users.FirstOrDefault(u => u.Id == id);
    }

    public IList<User> GetAll()
    {
        return _context.Users.ToList();
    }
    
    public bool Update(User data)
    {
        var user = GetById(data.Id);

        if (user is null)
            return false;

        user.FirstName = data.FirstName;
        user.SureName = data.SureName;

        return _context.SaveChanges() == 0;
    }

    public bool Delete(ulong id)
    {
        var user = GetById(id);

        if (user is null)
            return false;

        _context.Remove(user);

        return _context.SaveChanges() == 0;
    }
}