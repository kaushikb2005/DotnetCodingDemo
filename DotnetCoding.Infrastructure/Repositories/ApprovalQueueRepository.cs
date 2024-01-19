using DotnetCoding.Core.Interfaces;
using DotnetCoding.Core.Models;
using DotnetCoding.Core.Models.Dto;
using Microsoft.EntityFrameworkCore;
using System;

namespace DotnetCoding.Infrastructure.Repositories
{
    public class ApprovalQueueRepository : GenericRepository<ProductApprovalQueue>, IApprovalQueueRepository
    {
        protected readonly DbContextClass _dbContext;
        public ApprovalQueueRepository(DbContextClass dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<ProductQueueDto>> GetAllProductApprovalQueuesAsync()
        {
            // retrieving pending approval queue items
            var pendingApprovalQueue = await _dbContext.ProductApprovalQueues
                .Where(aq => !aq.Product.IsDeleted && aq.Product.ApprovalStatus == Constants.ApprovalStatusPending) // Filter for active products
                .OrderBy(aq => aq.RequestDate)
                .Select(aq => new ProductQueueDto
                {
                    ApprovalId = aq.ApprovalId,
                    ProductName = aq.Product.Name,
                    RequestReason = aq.RequestReason,
                    RequestDate = aq.RequestDate
                })                
                .ToListAsync();

            return pendingApprovalQueue;
        }
        public async Task<bool> ApproveRequest(int id)
        {
            var approvalQueueItem = _dbContext.ProductApprovalQueues.Find(id);
            if (approvalQueueItem == null)
            {
                return false;
            }

            // Additional logic for handling approval, e.g., update product status
            var product = _dbContext.Products.Find(approvalQueueItem.ProductId);
            if (product != null)
            {
                product.ApprovalStatus = Constants.ApprovalStatusApproved;
                product.IsActive = true;
                // Check if the request reason is 'Product Deletion request'
                if (approvalQueueItem.RequestReason == Constants.RequestReasonDelete)
                {
                    // Handle rejection of product deletion request, for example, change status back to 'Active'
                    product.IsActive = false;
                    product.IsDeleted = true;
                    product.DeletedDate = DateTime.UtcNow;
                    product.State = Constants.ProductStateDelete;
                }
                await _dbContext.SaveChangesAsync();
            }

            // Remove the item from the approval queue
            _dbContext.ProductApprovalQueues.Remove(approvalQueueItem);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> RejectRequest(int id)
        {
            var approvalQueueItem = _dbContext.ProductApprovalQueues.Find(id);
            if (approvalQueueItem == null)
            {
                return false;
            }

            // Additional logic for handling rejection, e.g., update product status
            var product = _dbContext.Products.Find(approvalQueueItem.ProductId);
            if (product != null)
            {
                product.ApprovalStatus = Constants.ApprovalStatusRejected;
                // Check if the request reason is 'Product Deletion request'
                if (approvalQueueItem.RequestReason == Constants.RequestReasonDelete)
                {
                    // Handle rejection of product deletion request, for example, change status back to 'Active'
                    product.IsActive = true;
                    product.IsDeleted = false;
                    product.DeletedDate = null;
                    product.State = Constants.ProductStateUpdate;
                }
                _dbContext.SaveChanges();
            }

            // Remove the item from the approval queue
            _dbContext.ProductApprovalQueues.Remove(approvalQueueItem);
            await _dbContext.SaveChangesAsync();

            return true;
        }
        public async Task<IEnumerable<ProductApprovalQueue>> GetByProductId(int productId)
        {
            return await _dbContext.ProductApprovalQueues.Where(p => p.ProductId == productId).ToListAsync();
        }
    }
}
