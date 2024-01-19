using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DotnetCoding.Core.Models
{
    public class Product : BaseEntity
    {
        [Key]
        public int ProductId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; } = string.Empty;
        [Required]
        public double Price { get; set; }
        public string State { get; set; } = string.Empty;
        public string ApprovalStatus { get; set; } = string.Empty;
        [DefaultValue(true)]
        public bool IsActive { get; set; } = false;
    }
}