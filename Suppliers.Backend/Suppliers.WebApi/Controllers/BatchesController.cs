using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Suppliers.Application.Batches.Commands.CreateBatch;
using Suppliers.Application.Batches.Commands.DeleteBatch;
using Suppliers.WebApi.Models.Batches;

namespace Suppliers.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("/api/[controller]")]
    public class BatchesController : BaseController
    {
        private readonly IMapper _mapper;
        public BatchesController(IMapper mapper) => _mapper = mapper;

        /// <summary>
        /// Creates the batch
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// POST /batches
        /// </remarks>
        /// <param name="createBatchDto">CreateBatchDto object</param>
        /// <returns>Returns id (guid)</returns>
        /// <response code="201">Success</response>
        /// <response code="401">If user is unauthorized</response>
        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateBatchDto createBatchDto)
        {
            var command = _mapper.Map<CreateBatchCommand>(createBatchDto);
            var batchId = await Mediator.Send(command);
            return Ok(batchId);
        }

        /// <summary>
        /// Deletes the batch by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// DELETE /orders/EE4A3BC4-D7B0-4C84-8653-01E9DFABCEEC/remove
        /// </remarks>
        /// <param name="id">Id of the batch (guid)</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="401">If user is unauthorized</response>
        [HttpDelete("{id}/remove")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> Delete(Guid id)
        {
            var command = new DeleteBatchCommand
            {
                BatchId = id
            };
            await Mediator.Send(command);
            return NoContent();
        }
    }
}
