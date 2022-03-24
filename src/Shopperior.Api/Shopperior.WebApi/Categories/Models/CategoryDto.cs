namespace Shopperior.WebApi.Categories.Models;

public class CategoryDto
{
    public Guid Guid { get; set; }
    public Guid ShoppingListGuid { get; set; }
    public string Name { get; set; }
}