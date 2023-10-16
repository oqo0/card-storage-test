using System.Net.Http.Headers;
using CardStorage.Models;
using CardStorage.Models.Requests;
using CardStorage.Services.Authentication;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

namespace CardStorage.Controllers;

[ApiController]
[Authorize]
[Route("api/auth")]
public class AuthenticationController : Controller
{
    #region Services

    private readonly IAuthenticateService _authenticationService;
    private readonly IValidator<AuthenticationRequest> _authenticationRequestValidator;

    #endregion

    #region Constructors

    public AuthenticationController(
        IAuthenticateService authenticationService,
        IValidator<AuthenticationRequest> authenticationRequestValidator
        )
    {
        _authenticationService = authenticationService;
        _authenticationRequestValidator = authenticationRequestValidator;
    }

    #endregion

    [HttpPost("login")]
    [AllowAnonymous]
    public IActionResult Login([FromBody] AuthenticationRequest authenticationRequest)
    {
        var validationResult = _authenticationRequestValidator.Validate(authenticationRequest);

        if (!validationResult.IsValid)
            return BadRequest(
                validationResult.ToDictionary()
            );
        
        var authenticationResponse = _authenticationService.Login(authenticationRequest);

        if (authenticationResponse.Status != AuthenticationStatus.Success)
            return Ok(authenticationResponse.Status.ToString());
        
        Response.Headers.Add("X-Session-Token", authenticationResponse.SessionInfo.Token);
        
        return Ok(authenticationResponse);
    }
    
    [HttpGet("session")]
    public IActionResult GetSessionInfo()
    {
        var authorization = Request.Headers[HeaderNames.Authorization];

        if (!AuthenticationHeaderValue.TryParse(authorization, out var authHeaderValue))
            return Unauthorized("Invalid authorization header");

        var authScheme = authHeaderValue.Scheme;
        var sessionToken = authHeaderValue.Parameter;

        if (authScheme != "Bearer")
            return Unauthorized("Invalid authorization scheme. Only \"Bearer\" is supported.");
        if (string.IsNullOrEmpty(sessionToken))
            return Unauthorized("Invalid authorization token. Session token is required.");

        var sessionInfo = _authenticationService.GetSessionInfo(sessionToken);

        if (sessionInfo is null)
            return Unauthorized("Session was not found");

        return Ok(sessionInfo);
    }
}