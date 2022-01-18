using Shopperior.Domain.Contracts.Shared.Cqrs;
using Shopperior.Domain.Contracts.ShoppingLists.Models;
using StratmanMedia.ResponseObjects;

namespace Shopperior.Domain.Contracts.ShoppingLists.Commands;

public interface IDeleteShoppingListCommand : IBaseCommand<IDeleteShoppingListRequest>
{

}