namespace Core_MaktabShop.Domain.Core._common
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UptatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
