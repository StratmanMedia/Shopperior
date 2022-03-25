namespace Shopperior.Domain.Contracts.Categories.Models;

public interface ICategoryModel
{
    public Guid Guid { get; set; }
    public Guid ShoppingListGuid { get; set; }
    public string Name { get; set; }
}