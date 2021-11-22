using System.Threading.Tasks;

namespace Shopperior.Domain.Contracts.Categories
{
    public interface ICreateCategoryCommand
    {
        Task ExecuteAsync(ICreateCategoryRequest request);
    }
}