using System;
using System.Collections.Generic;
using System.Linq;
using CardStorage.Data;
using CardStorage.Data.Models;
using Microsoft.Extensions.Logging;

namespace CardStorage.Services.Repository.Impl;

public class CardRepository : ICardRepository
{
    #region Services

    private readonly CardStorageDbContext _context;
    private readonly ILogger<CardRepository> _logger;

    #endregion

    #region Constructors

    public CardRepository(
        CardStorageDbContext context,
        ILogger<CardRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    #endregion

    public Card Create(Card data)
    {
        _context.Cards.Add(data);
        _context.SaveChanges();
        return data;
    }

    public Card? GetById(Guid id)
    {
        return _context.Cards.FirstOrDefault(c => c.Id == id);
    }

    public IList<Card> GetAll()
    {
        return _context.Cards.ToList();
    }

    public bool Update(Card data)
    {
        var card = GetById(data.Id);

        if (card is null)
            return false;

        card.OwnerId = data.OwnerId;
        card.Number = data.Number;
        card.CVV2 = data.CVV2;
        card.OwnerName = data.OwnerName;
        card.ExpDateTime = data.ExpDateTime;

        return _context.SaveChanges() == 0;
    }

    public bool Delete(Guid id)
    {
        var card = GetById(id);

        if (card is null)
            return false;

        _context.Remove(card);

        return _context.SaveChanges() == 0;
    }

    public IList<Card> GetAllByUser(ulong userId)
    {
        return _context.Cards.Where(x => x.OwnerId == userId).ToList();
    }
}