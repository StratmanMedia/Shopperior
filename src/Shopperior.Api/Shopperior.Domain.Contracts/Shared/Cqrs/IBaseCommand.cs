namespace Shopperior.Domain.Contracts.Shared.Cqrs;

public interface IBaseCommand<TRequest>
{
    Task ExecuteAsync(TRequest request);
}