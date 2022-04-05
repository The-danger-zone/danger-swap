namespace DangerSwap.Models
{
    public interface IEntityBase
    {
        public string Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}
