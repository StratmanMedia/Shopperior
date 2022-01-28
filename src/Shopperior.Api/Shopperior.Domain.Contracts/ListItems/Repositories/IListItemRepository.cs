using Shopperior.Domain.Entities;
using StratmanMedia.Repositories.EFCore;

namespace Shopperior.Domain.Contracts.ListItems.Repositories
{
    public interface IListItemRepository : IRepository<ListItem>
    {
        Task<ListItem> GetOne(long id);
        Task<ListItem> GetOne(Guid guid);
    }
}