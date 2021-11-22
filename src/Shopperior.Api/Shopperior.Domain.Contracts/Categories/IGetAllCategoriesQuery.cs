using System.Collections.Generic;
using System.Threading.Tasks;
using Shopperior.Domain.Entities;

namespace Shopperior.Domain.Contracts.Categories
{
    public interface IGetAllCategoriesQuery
    {
        Task<IEnumerable<Category>> ExecuteAsync();
    }
}