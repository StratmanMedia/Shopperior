using Shopperior.Domain.Entities;

namespace Shopperior.Domain.Contracts.Categories.Models;

public interface ICategoryModel
{
    public Guid Guid { get; set; }
    public User User { get; set; }
    public string Name { get; set; }
}