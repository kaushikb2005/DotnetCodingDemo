using DotnetCoding.Core.Interfaces;
using DotnetCoding.Core.Models;
using DotnetCoding.Core.Models.Dto;
using DotnetCoding.Services.Interfaces;

namespace DotnetCoding.Services
{
    public class ApprovalQueueService : IApprovalQueueService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ApprovalQueueService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<ProductQueueDto>> GetAllProductApprovalQueuesAsync()
        {
           return await _unitOfWork.ApprovalQueues.GetAllProductApprovalQueuesAsync();
        }

        public async Task<ProductApprovalQueue> GetApprovalQueueById(int id)
        {
            ProductApprovalQueue queueDetails = new ProductApprovalQueue();
            if (id > 0)
            {
                queueDetails = await _unitOfWork.ApprovalQueues.GetById(id);                
            }
            return queueDetails;
        }

        public async Task<bool> ApproveRequest(int id)
        {
            return await _unitOfWork.ApprovalQueues.ApproveRequest(id);
        }
        public async Task<bool> RejectRequest(int id)
        {
            return await _unitOfWork.ApprovalQueues.RejectRequest(id);
        }
    }
}