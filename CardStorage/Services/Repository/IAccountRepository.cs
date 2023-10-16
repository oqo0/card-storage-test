using CardStorage.Data.Models;

namespace CardStorage.Services.Repository;

public interface IAccountRepository : IRepository<Account, long>
{
    Account? FindAccountByLogin(string login);
}