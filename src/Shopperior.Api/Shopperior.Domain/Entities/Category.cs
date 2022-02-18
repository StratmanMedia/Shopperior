namespace Shopperior.Domain.Entities
{
    public class Category
    {
        public long Id { get; set; }
        public Guid Guid { get; set; }
        public long UserId { get; set; }
        public User User { get; set; }
        public string Name { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime? LastModifiedTime { get; set; }
        public DateTime? TrashedTime { get; set; }
    }
}