﻿using HC.Shared.Constants;
using HC.Shared.Dtos.User;
using HC.Shared.Markers;
using HC.Shared.Models;
using HC.Shared.Services.Contracts;
using HC.Web.Services.Contracts;

namespace HC.Web.Services.Implementations;

public class UserService : IUserService, IScopedDependency
{
    private readonly IApiCaller _apiCaller;

    public UserService(IApiCaller apiCaller)
    {
        _apiCaller = apiCaller;
    }

    public async Task<Result<List<UserResponseDto>>> GetAll(CancellationToken cancellationToken = default)
    {
        var response = await _apiCaller.GetAsync<List<UserResponseDto>>(RoutingConstants.ServerSide.User.GetAll, cancelationToken: cancellationToken);
        if (response.Succeeded is false) return Result.Failed<List<UserResponseDto>>(response.Message);
        return response;
    }

    public async Task<Result<UserResponseDto>> GetById(int id, CancellationToken cancellationToken = default)
    {
        var response = await _apiCaller.GetAsync<UserResponseDto>(RoutingConstants.ServerSide.User.GetById);
        if (response.Succeeded is false) return Result.Failed<UserResponseDto>(response.Message);
        return response;
    }
}
