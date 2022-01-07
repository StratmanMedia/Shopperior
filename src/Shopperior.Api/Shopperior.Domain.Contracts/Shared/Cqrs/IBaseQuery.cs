﻿namespace Shopperior.Domain.Contracts.Shared.Cqrs;

public interface IBaseQuery<TResponse>
{
    Task<TResponse> ExecuteAsync(CancellationToken cancellationToken = new CancellationToken());
}

public interface IBaseQuery<TRequest, TResponse>
{
    Task<TResponse> ExecuteAsync(TRequest request, CancellationToken cancellationToken = new CancellationToken());
}