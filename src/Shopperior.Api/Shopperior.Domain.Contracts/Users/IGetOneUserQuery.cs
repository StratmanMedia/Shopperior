using Shopperior.Domain.Contracts.Shared.Cqrs;
using Shopperior.Domain.Entities;
using Shopperior.Domain.ValueObjects;

namespace Shopperior.Domain.Contracts.Users
{
    public interface IGetOneUserQuery :
        IBaseQuery<long, User>,
        IBaseQuery<Guid, User>,
        IBaseQuery<string, User>,
        IBaseQuery<EmailAddress, User>
    {

    }
}