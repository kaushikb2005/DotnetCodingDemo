namespace DotnetCoding.Core.Models.Dto
{
    public class ProductSearchRequestDto
    {
        public string? ProductName { get; set; } = string.Empty;
        public double? MinPrice { get; set; }
        public double? MaxPrice { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
