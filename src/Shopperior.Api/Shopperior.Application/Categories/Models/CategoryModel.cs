using Shopperior.Domain.Contracts.Categories.Models;
using Shopperior.Domain.Entities;

namespace Shopperior.Application.Categories.Models;

public class CategoryModel : ICategoryModel
{
    public Guid Guid { get; set; }
    public User User { get; set; }
    public string Name { get; set; }
}