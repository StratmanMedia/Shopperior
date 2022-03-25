using Shopperior.Domain.Contracts.Categories.Models;

namespace Shopperior.Application.Categories.Models;

public class CategoryModel : ICategoryModel
{
    public Guid Guid { get; set; }
    public Guid ShoppingListGuid { get; set; }
    public string Name { get; set; }
}