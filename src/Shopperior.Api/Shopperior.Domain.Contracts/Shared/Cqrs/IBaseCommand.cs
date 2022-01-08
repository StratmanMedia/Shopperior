using StratmanMedia.ResponseObjects;

namespace Shopperior.Domain.Contracts.Shared.Cqrs;

public interface IBaseCommand<TRequest>
{
    Task<Response> ExecuteAsync(TRequest request, CancellationToken cancellationToken);
}