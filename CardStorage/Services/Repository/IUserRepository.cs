using CardStorage.Data.Models;

namespace CardStorage.Services.Repository;

public interface IUserRepository : IRepository<User, ulong>
{
    
}