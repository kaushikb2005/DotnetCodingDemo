using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotnetCoding.Core.Models
{
    public class ProductApprovalQueue
    {
        [Key]
        public int ApprovalId { get; set; }
        [Required]
        [ForeignKey("ProductId")]
        public Product? Product { get; set; }
        public int ProductId { get; set; }
        public string RequestReason { get; set; } = string.Empty;        
        [DefaultValue(true)]
        public DateTime RequestDate { get; set; } = DateTime.UtcNow;
    }
}
