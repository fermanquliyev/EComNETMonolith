namespace EComNetMonolith.Shared.DDD
{
    public abstract class Entity<TPrimaryKey> : IEntity<TPrimaryKey>
    {
        public TPrimaryKey Id { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletionTime { get; set; }
        public string? CreatorUserId { get; set; }
        public string? LastModifierUserId { get; set; }
        public string? DeleterUserId { get; set; }
    }
}
