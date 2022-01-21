namespace Shopperior.Domain.Contracts.Shared.Cqrs;

public interface IBaseQuery<TResponse>
{
    Task<TResponse> ExecuteAsync(CancellationToken ct = new());
}

public interface IBaseQuery<TRequest, TResponse>
{
    Task<TResponse> ExecuteAsync(TRequest request, CancellationToken ct = new());
}