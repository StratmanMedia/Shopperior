using System.Threading.Tasks;
using StratmanMedia.ResponseObjects;

namespace Shopperior.Domain.Contracts.Users
{
    public interface ICreateUserCommand
    {
        Task<Response> ExecuteAsync(ICreateUserRequest request);
    }
}