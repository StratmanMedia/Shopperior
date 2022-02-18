using Shopperior.Domain.Contracts.Categories.Models;

namespace Shopperior.Domain.Contracts.Categories.Commands
{
    public interface ICreateCategoryCommand
    {
        Task ExecuteAsync(ICategoryModel request, CancellationToken ct = default);
    }
}