namespace DotnetCoding.Core.Models.Dto
{
    public class ProductQueueDto
    {
        public int ApprovalId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string RequestReason { get; set; } = string.Empty;
        public DateTime RequestDate { get; set; }
    }
}