using Shopperior.Domain.Contracts.ShoppingLists.Models;
using Shopperior.Domain.Entities;

namespace Shopperior.Domain.Contracts.ShoppingLists.Resolvers;

public interface IListPermissionModelResolver
{
    Task<IUserListPermissionModel> ResolveAsync(UserListPermission entity);
    Task<UserListPermission> ResolveAsync(IUserListPermissionModel model);
}