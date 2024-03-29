﻿using HC.Domain.Contracts;
using HC.Shared.Constants;
using HC.Shared.Dtos.Auth;
using HC.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HC.Api.Controllers.v1;

[ApiVersion("1.0")]
public class AuthController : BaseController
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authRepository)
    {
        _authService = authRepository;
    }

    [AllowAnonymous]
    [HttpPost(RoutingConstants.ServerSide.Auth.SignUp)]
    public virtual async Task<Result> SignUp([FromBody] SignUpRequestDto request, CancellationToken cancellationToken = default)
    {
        await _authService.SignUp(request, cancellationToken);
        return Result.Success();
    }

    [AllowAnonymous]
    [HttpPost(RoutingConstants.ServerSide.Auth.SignIn)]
    public virtual async Task<Result<SignInResponseDto>> SignIn([FromBody] SignInRequestDto request, CancellationToken cancellationToken = default)
    {
        var result = await _authService.SignIn(request, cancellationToken);
        return Result.Success(result.Data);
    }
}
