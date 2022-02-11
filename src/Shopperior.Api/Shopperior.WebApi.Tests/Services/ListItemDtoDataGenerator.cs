using Shopperior.Domain.Enumerations;
using Shopperior.WebApi.ShoppingLists.Models;

namespace Shopperior.WebApi.Tests.Services;

public class ListItemDtoDataGenerator
{
    private readonly RandomDataGenerator _random = new();

    public IEnumerable<ListItemDto> Generate(long numberToGenerate, Guid listGuid = default)
    {
        var items = new List<ListItemDto>();
        for (var j = 0; j < numberToGenerate; j++)
        {
            var quantity = _random.RandomDouble(0, 100);
            var unitPrice = _random.RandomDecimal(2, 0, 100);
            var isInCart = _random.RandomBoolean();
            var hasPurchased = isInCart && _random.RandomBoolean();
            items.Add(new ListItemDto
            {
                Guid = Guid.NewGuid(),
                ShoppingListGuid = (listGuid == default) ? Guid.NewGuid() : listGuid,
                Name = _random.RandomString(16),
                Brand = _random.RandomString(16),
                Comment = _random.RandomString(16),
                Quantity = quantity,
                Measurement = Measurement.FromValue(_random.RandomInt(0, 15)).ToString(),
                UnitPrice = unitPrice,
                TotalPrice = (decimal)quantity * unitPrice,
                IsInCart = isInCart,
                HasPurchased = hasPurchased
            });
        }

        return items;
    }
}