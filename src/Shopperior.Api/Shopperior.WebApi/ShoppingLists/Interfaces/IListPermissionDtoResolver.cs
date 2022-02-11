using Shopperior.Domain.Contracts.ShoppingLists.Models;
using Shopperior.WebApi.ShoppingLists.Models;

namespace Shopperior.WebApi.ShoppingLists.Interfaces;

public interface IListPermissionDtoResolver
{
    Task<UserListPermissionDto> ResolveAsync(IUserListPermissionModel model, CancellationToken ct = default);
}