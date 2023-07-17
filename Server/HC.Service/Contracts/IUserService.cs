﻿using HC.Shared.Dtos.User;

namespace HC.Service.Contracts;

public interface IUserService
{
    Task<List<UserResponseDto>> GetAll(CancellationToken cancellationToken = default);
    Task<UserResponseDto> GetById(int id, CancellationToken cancellationToken = default);
}
