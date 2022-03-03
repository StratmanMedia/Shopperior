using Microsoft.Extensions.Logging;
using Shopperior.Domain.Contracts.ListItems.Commands;
using Shopperior.Domain.Contracts.ListItems.Models;
using Shopperior.Domain.Contracts.ListItems.Repositories;
using Shopperior.Domain.Contracts.ShoppingLists.Resolvers;

namespace Shopperior.Application.ListItems.Commands;

public class CreateListItemCommand : ICreateListItemCommand
{
    private readonly ILogger<CreateListItemCommand> _logger;
    private readonly IListItemModelResolver _listItemModelResolver;
    private readonly IListItemRepository _listItemRepository;

    public CreateListItemCommand(
        ILogger<CreateListItemCommand> logger,
        IListItemModelResolver listItemModelResolver,
        IListItemRepository listItemRepository)
    {
        _logger = logger;
        _listItemModelResolver = listItemModelResolver;
        _listItemRepository = listItemRepository;
    }

    public async Task ExecuteAsync(IListItemModel request, CancellationToken ct = default)
    {
        await ValidateRequest(request);
        var listItem = await _listItemModelResolver.ResolveAsync(request);
        if (listItem == null)
            throw new ArgumentException(nameof(request));

        listItem.Guid = Guid.NewGuid();
        listItem.CreatedTime = DateTime.UtcNow;
        await _listItemRepository.CreateAsync(listItem, ct);
    }

    private async Task ValidateRequest(IListItemModel request)
    {
        await Task.Run(() =>
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if (request.ShoppingListGuid == Guid.Empty)
                throw new ArgumentOutOfRangeException(nameof(request.ShoppingListGuid));

            if (request.Quantity <= 0)
                throw new ArgumentOutOfRangeException(nameof(request.Quantity));
        });
    }
}