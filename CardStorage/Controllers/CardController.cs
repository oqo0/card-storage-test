using System;
using System.Collections.Generic;
using AutoMapper;
using CardStorage.Data.Models;
using CardStorage.Models;
using CardStorage.Models.Requests;
using CardStorage.Models.Responses;
using CardStorage.Services.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CardStorage.Controllers;

[Authorize]
public class CardController : Controller
{
    #region Services

    private readonly ILogger<CardController> _logger;
    private readonly ICardRepository _cardRepository;
    private readonly IMapper _mapper;

    #endregion

    #region Constructor

    public CardController(
        ILogger<CardController> logger,
        ICardRepository cardRepository, IMapper mapper)
    {
        _logger = logger;
        _cardRepository = cardRepository;
        _mapper = mapper;
    }

    #endregion

    #region Public Methods

    [HttpPost("create")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    public IActionResult Create([FromBody] CreateCardRequest request)
    {
        try
        {
            var cardId = _cardRepository.Create(_mapper.Map<Card>(request));
            
            return Ok(new CreateCardResponse
            {
                Id = cardId.Id
            });
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Error while creating a new card.");
            return Ok(new CreateCardResponse
            {
                ErrorCode = 1012,
                ErrorMessage = "Server error."
            });
        }
    }

    [HttpGet("get-by-client-id")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    public IActionResult GetByClientId(ulong clientId)
    {
        try
        {
            var cards = _cardRepository.GetAllByUser(clientId);

            return Ok(new GetCardsResponse
            {
                Cards = _mapper.Map<IList<CardDto>>(cards)
            });
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Error while getting cards by client id.");
            return Ok(new CreateCardResponse
            {
                ErrorCode = 1013,
                ErrorMessage = "Server error."
            });
        }
    }
    
    [HttpGet("get-by-id")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    public IActionResult GetById(Guid cardId)
    {
        try
        {
            var card = _cardRepository.GetById(cardId);

            if (card is null)
            {
                return Ok(new GetCardsResponse
                {
                    ErrorCode = 2101,
                    ErrorMessage = "Cant find it"
                });
            }
            
            return Ok(new GetCardByIdResponse
            {
                Card = new CardDto
                {
                    CVV2 = card.CVV2,
                    ExpDateTime = card.ExpDateTime.ToString("MM/yy"),
                    Number = card.Number,
                    OwnerName = card.OwnerName
                }
            });
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Error while trying to get card by id");
            return Ok(new CreateCardResponse
            {
                ErrorCode = 1030,
                ErrorMessage = "Server error."
            });
        }
    }
    
    #endregion
    
    
    // ---------------------------------------------------------------------------
    // TESTING METHODS
    // ---------------------------------------------------------------------------
    
    [HttpHead("get-by-client-id")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    public IActionResult GetByClientIdHead(ulong clientId)
    {
        try
        {
            var cards = _cardRepository.GetAllByUser(clientId);

            return Ok(new GetCardsResponse
            {
                Cards = _mapper.Map<IList<CardDto>>(cards)
            });
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Error while getting cards by client id.");
            return Ok(new CreateCardResponse
            {
                ErrorCode = 1013,
                ErrorMessage = "Server error."
            });
        }
    }
    
    [HttpPut("get-by-client-id")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    public IActionResult GetByClientIdPut(ulong clientId)
    {
        try
        {
            var cards = _cardRepository.GetAllByUser(clientId);

            return Ok(new GetCardsResponse
            {
                Cards = _mapper.Map<IList<CardDto>>(cards)
            });
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Error while getting cards by client id.");
            return Ok(new CreateCardResponse
            {
                ErrorCode = 1013,
                ErrorMessage = "Server error."
            });
        }
    }
    
    [HttpDelete("get-by-client-id")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    public IActionResult GetByClientIdDelete(ulong clientId)
    {
        try
        {
            var cards = _cardRepository.GetAllByUser(clientId);

            return Ok(new GetCardsResponse
            {
                Cards = _mapper.Map<IList<CardDto>>(cards)
            });
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Error while getting cards by client id.");
            return Ok(new CreateCardResponse
            {
                ErrorCode = 1013,
                ErrorMessage = "Server error."
            });
        }
    }
    
    [HttpOptions("get-by-client-id")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    public IActionResult GetByClientIdOptions(ulong clientId)
    {
        try
        {
            var cards = _cardRepository.GetAllByUser(clientId);

            return Ok(new GetCardsResponse
            {
                Cards = _mapper.Map<IList<CardDto>>(cards)
            });
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Error while getting cards by client id.");
            return Ok(new CreateCardResponse
            {
                ErrorCode = 1013,
                ErrorMessage = "Server error."
            });
        }
    }
    
    [HttpPatch("get-by-client-id")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    public IActionResult GetByClientIdPatch(ulong clientId)
    {
        try
        {
            var cards = _cardRepository.GetAllByUser(clientId);

            return Ok(new GetCardsResponse
            {
                Cards = _mapper.Map<IList<CardDto>>(cards)
            });
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Error while getting cards by client id.");
            return Ok(new CreateCardResponse
            {
                ErrorCode = 1013,
                ErrorMessage = "Server error."
            });
        }
    }
}