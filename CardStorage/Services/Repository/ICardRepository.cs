using System;
using System.Collections.Generic;
using CardStorage.Data.Models;

namespace CardStorage.Services.Repository;

public interface ICardRepository : IRepository<Card, Guid>
{
    IList<Card> GetAllByUser(ulong userId);
}