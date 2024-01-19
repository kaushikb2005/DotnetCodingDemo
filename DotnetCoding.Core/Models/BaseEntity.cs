using System.ComponentModel;

namespace DotnetCoding.Core.Models
{
    public abstract class BaseEntity
    {
        [DefaultValue(true)]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        [DefaultValue(true)]
        public DateTime ModifiedDate { get; set; } = DateTime.UtcNow;        
        public DateTime? DeletedDate {  get; set; }
        [DefaultValue(true)]
        public bool IsDeleted { get; set; } = false;
    }
}