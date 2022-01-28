using Shopperior.Domain.Contracts.ListItems.Models;
using Shopperior.Domain.Contracts.Shared.Cqrs;

namespace Shopperior.Domain.Contracts.ListItems.Commands;

public interface ICreateListItemCommand
{
    Task ExecuteAsync(IListItemModel request, CancellationToken ct = new CancellationToken());
}