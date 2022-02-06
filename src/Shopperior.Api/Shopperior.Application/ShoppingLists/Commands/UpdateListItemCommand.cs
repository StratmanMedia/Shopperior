using Shopperior.Domain.Contracts.ListItems.Models;
using Shopperior.Domain.Contracts.ListItems.Repositories;
using Shopperior.Domain.Contracts.ShoppingLists.Commands;
using Shopperior.Domain.Contracts.ShoppingLists.Resolvers;

namespace Shopperior.Application.ShoppingLists.Commands;

public class UpdateListItemCommand : IUpdateListItemCommand
{
    private readonly IListItemModelResolver _listItemModelResolver;
    private readonly IListItemRepository _listItemRepository;

    public UpdateListItemCommand(
        IListItemModelResolver listItemModelResolver,
        IListItemRepository listItemRepository)
    {
        _listItemModelResolver = listItemModelResolver;
        _listItemRepository = listItemRepository;
    }

    public async Task ExecuteAsync(IListItemModel request, CancellationToken ct = new CancellationToken())
    {
        await ValidateRequest(request, ct);
        var entity = await _listItemModelResolver.ResolveAsync(request);
        entity.LastModifiedTime = DateTime.UtcNow;
        await _listItemRepository.UpdateAsync(entity, ct);
    }

    private Task ValidateRequest(IListItemModel request, CancellationToken ct = new CancellationToken())
    {
        if (request == null) 
            throw new ArgumentNullException(nameof(request));

        if (request.Guid == Guid.Empty)
            throw new ArgumentOutOfRangeException(nameof(request.Guid));

        return Task.CompletedTask;
    }
}