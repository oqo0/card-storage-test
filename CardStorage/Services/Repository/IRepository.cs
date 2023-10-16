using System.Collections.Generic;

namespace CardStorage.Services.Repository;

public interface IRepository<T, in TId>
{
    T Create(T data);

    T? GetById(TId id);

    IList<T> GetAll();
    
    bool Update(T data);

    bool Delete(TId id);
}