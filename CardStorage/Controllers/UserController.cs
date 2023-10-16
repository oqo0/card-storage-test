using System;
using CardStorage.Data.Models;
using CardStorage.Models.Requests;
using CardStorage.Models.Responses;
using CardStorage.Services;
using CardStorage.Services.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CardStorage.Controllers;

[Authorize]
public class UserController : Controller
{
    #region Services

    private readonly IUserRepository _userRepository;
    private readonly ILogger<UserController> _logger;

    #endregion

    #region Constructors

    public UserController(
        IUserRepository userRepository,
        ILogger<UserController> logger)
    {
        _userRepository = userRepository;
        _logger = logger;
    }

    #endregion

    #region Public Methods

    [HttpPost("create-user")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    public IActionResult CreateUser([FromBody] CreateClientRequest request) 
    {
        try
        {
            var user = _userRepository.Create(
                new User
                {
                    FirstName = request.FirstName,
                    SureName = request.SureName
                });

            return Ok(new CreateClientResponse
            {
                ClientId = user.Id
            });
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Error while creating a new user");
            
            return Ok(new CreateClientResponse
            {
                ErrorCode = 1203,
                ErrorMessage = "Server error"
            });
        }
    }

    [HttpPost("change-user")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    public IActionResult ChangeUser([FromBody] ChangeClientRequest request) 
    {
        try
        {
            bool operationStatus = _userRepository.Update(new User()
            {
                Id = request.UserId,
                FirstName = request.FirstName,
                SureName = request.SureName
            });
            
            return Ok(new ChangeClientResponse
            {
                
                Status = operationStatus
            });
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Error while changing a user");
            
            return Ok(new CreateClientResponse
            {
                ErrorCode = 1202,
                ErrorMessage = "Server error"
            });
        }
    }
    
    #endregion
}