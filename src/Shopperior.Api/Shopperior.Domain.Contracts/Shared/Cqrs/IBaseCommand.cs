using StratmanMedia.ResponseObjects;

namespace Shopperior.Domain.Contracts.Shared.Cqrs;

public interface IBaseCommand<TRequest>
{
    Task<Response> ExecuteAsync(TRequest request, CancellationToken cancellationToken);
}

public interface IBaseCommand<TRequest, TResponse>
{
    Task<Response<TResponse>> ExecuteAsync(TRequest request, CancellationToken cancellationToken);
}