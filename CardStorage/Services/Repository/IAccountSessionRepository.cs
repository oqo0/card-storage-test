using CardStorage.Data.Models;

namespace CardStorage.Services.Repository;

public interface IAccountSessionRepository : IRepository<AccountSession, long>
{
    AccountSession? GetByToken(string token);
}