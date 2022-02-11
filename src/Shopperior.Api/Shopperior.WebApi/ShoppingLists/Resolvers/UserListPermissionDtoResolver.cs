using Shopperior.Domain.Contracts.ShoppingLists.Models;
using Shopperior.WebApi.ShoppingLists.Interfaces;
using Shopperior.WebApi.ShoppingLists.Models;
using Shopperior.WebApi.Users.Interfaces;

namespace Shopperior.WebApi.ShoppingLists.Resolvers;

public class ListPermissionDtoResolver : IListPermissionDtoResolver
{
    private readonly IUserDtoResolver _userDtoResolver;

    public ListPermissionDtoResolver(
        IUserDtoResolver userDtoResolver)
    {
        _userDtoResolver = userDtoResolver;
    }

    public async Task<UserListPermissionDto> ResolveAsync(IUserListPermissionModel model, CancellationToken ct = default)
    {
        var userDto = _userDtoResolver.Resolve(model.User);
        var dto = new UserListPermissionDto
        {
            User = userDto,
            ShoppingListGuid = model.ShoppingListGuid,
            Permission = model.Permission
        };

        return await Task.FromResult(dto);
    }
}