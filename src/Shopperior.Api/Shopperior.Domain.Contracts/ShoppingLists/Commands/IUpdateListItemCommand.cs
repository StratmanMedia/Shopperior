using Shopperior.Domain.Contracts.ListItems.Models;

namespace Shopperior.Domain.Contracts.ShoppingLists.Commands;

public interface IUpdateListItemCommand
{
    Task ExecuteAsync(IListItemModel request, CancellationToken ct = new CancellationToken());
}