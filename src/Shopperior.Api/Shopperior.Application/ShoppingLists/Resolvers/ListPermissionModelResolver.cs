using Shopperior.Application.ShoppingLists.Models;
using Shopperior.Domain.Contracts.ShoppingLists.Models;
using Shopperior.Domain.Contracts.ShoppingLists.Repositories;
using Shopperior.Domain.Contracts.ShoppingLists.Resolvers;
using Shopperior.Domain.Contracts.Users.Repositories;
using Shopperior.Domain.Entities;

namespace Shopperior.Application.ShoppingLists.Resolvers;

public class ListPermissionModelResolver : IListPermissionModelResolver
{
    private readonly IUserRepository _userRepository;
    private readonly IShoppingListRepository _shopperListRepository;

    public ListPermissionModelResolver(
        IUserRepository userRepository,
        IShoppingListRepository shopperListRepository)
    {
        _userRepository = userRepository;
        _shopperListRepository = shopperListRepository;
    }

    public async Task<IUserListPermissionModel> ResolveAsync(UserListPermission entity)
    {
        var user = await _userRepository.GetAsync(entity.UserId);
        var list = await _shopperListRepository.GetOneAsync(entity.ShoppingListId);
        var model = new UserListPermissionModel
        {
            User = user,
            ShoppingListGuid = list.Guid,
            Permission = entity.Permission
        };

        return model;
    }

    public Task<UserListPermission> ResolveAsync(IUserListPermissionModel model)
    {
        throw new NotImplementedException();
    }
}