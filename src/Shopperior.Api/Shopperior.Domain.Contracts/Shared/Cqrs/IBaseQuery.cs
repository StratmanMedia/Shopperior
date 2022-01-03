namespace Shopperior.Domain.Contracts.Shared.Cqrs;

public interface IBaseQuery<TResponse>
{
    Task<TResponse> ExecuteAsync();
}

public interface IBaseQuery<TRequest, TResponse>
{
    Task<TResponse> ExecuteAsyc(TRequest request);
}