using DotnetCoding.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DotnetCoding.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductApprovalQueueController : ControllerBase
    {
        public readonly IApprovalQueueService _approvalQueueService;
        public ProductApprovalQueueController(IApprovalQueueService approvalQueueService)
        {
            _approvalQueueService = approvalQueueService;
        }

        /// <summary>
        /// Get the list of product to approve
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetApprovalQueueList()
        {
            var queueList = await _approvalQueueService.GetAllProductApprovalQueuesAsync();
            if (queueList == null)
            {
                return NotFound();
            }
            return Ok(queueList);
        }

        [HttpPost("{id}/approve")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(400)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> ApproveRequest(int id)
        {
            var approvalQueueItem = await _approvalQueueService.GetApprovalQueueById(id);
            if (approvalQueueItem == null)
            {
                return NotFound();
            }

            var isRequestApproved = await _approvalQueueService.ApproveRequest(id);

            if (isRequestApproved)
            {
                return Ok(isRequestApproved);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost("{id}/reject")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(400)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> RejectRequest(int id)
        {
            var approvalQueueItem = await _approvalQueueService.GetApprovalQueueById(id);
            if (approvalQueueItem == null)
            {
                return NotFound();
            }

            var isRequestRejected = await _approvalQueueService.RejectRequest(id);

            if (isRequestRejected)
            {
                return Ok(isRequestRejected);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
