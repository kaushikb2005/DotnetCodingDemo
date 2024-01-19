namespace DotnetCoding.Core.Models.Dto
{
    public class Constants
    {
        public const string ApprovalStatusPending = "Pending";
        public const string ApprovalStatusApproved = "Approved";
        public const string ApprovalStatusRejected = "Rejected";

        public const string ProductStateCreate = "Create";
        public const string ProductStateUpdate = "Update";
        public const string ProductStateDelete = "Delete";

        public const string RequestReasonDelete = "Product deletion request";
        public const string ProductIncreaseBy50Percent = "Price increase exceeds 50% of the previous price";
        public const string ProductExceeds5000 = "Price exceeds $5000 at the time of creation";
    }
}