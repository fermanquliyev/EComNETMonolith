namespace EComNetMonolith.Shared.DDD
{
    public interface IEntity<TPrimaryKey> : IEntity
    {
        public TPrimaryKey Id { get; set; }
    }

    public interface IEntity
    {
        public DateTime CreationTime { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletionTime { get; set; }
        public string? CreatorUserId { get; set; }
        public string? LastModifierUserId { get; set; }
        public string? DeleterUserId { get; set; }
    }
}
