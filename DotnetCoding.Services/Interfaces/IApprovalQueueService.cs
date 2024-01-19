using DotnetCoding.Core.Models;
using DotnetCoding.Core.Models.Dto;

namespace DotnetCoding.Services.Interfaces
{
    public interface IApprovalQueueService
    {
        Task<ProductApprovalQueue> GetApprovalQueueById(int approvalId);
        Task<IEnumerable<ProductQueueDto>> GetAllProductApprovalQueuesAsync();
        Task<bool> ApproveRequest(int productId);
        Task<bool> RejectRequest(int productId);
    }
}