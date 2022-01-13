using Shopperior.Domain.Contracts.Shared.Cqrs;
using Shopperior.Domain.Entities;

namespace Shopperior.Domain.Contracts.Users
{
    public interface ICreateUserCommand : IBaseCommand<User, User>
    {
    }
}