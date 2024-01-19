using DotnetCoding.Core.Models;
using DotnetCoding.Core.Models.Dto;
namespace DotnetCoding.Core.Interfaces
{
    public interface IApprovalQueueRepository : IGenericRepository<ProductApprovalQueue>
    {
        Task<IEnumerable<ProductApprovalQueue>> GetByProductId(int productId);
        Task<IEnumerable<ProductQueueDto>> GetAllProductApprovalQueuesAsync();
        Task<bool> ApproveRequest(int id);
        Task<bool> RejectRequest(int id);
    }
}