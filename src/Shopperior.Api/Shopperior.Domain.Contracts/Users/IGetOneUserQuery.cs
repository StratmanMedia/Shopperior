using Shopperior.Domain.Contracts.Shared.Cqrs;
using Shopperior.Domain.Entities;

namespace Shopperior.Domain.Contracts.Users
{
    public interface IGetOneUserQuery :
        IBaseQuery<long, User>,
        IBaseQuery<Guid, User>,
        IBaseQuery<string, User>
    {

    }
}