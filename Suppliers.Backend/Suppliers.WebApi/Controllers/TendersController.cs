using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Suppliers.Application.Tenders.Commands.CloseTender;
using Suppliers.Application.Tenders.Commands.CreateTender;
using Suppliers.Application.Tenders.Commands.DeleteTender;
using Suppliers.Application.Tenders.Commands.RegisterInTender;
using Suppliers.Application.Tenders.Commands.UpdateTender;
using Suppliers.Application.Tenders.Queries.GetTenderDetails;
using Suppliers.Application.Tenders.Queries.GetTendersList;
using Suppliers.WebApi.Models.Tenders;

namespace Suppliers.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class TendersController : BaseController
    {
        private readonly IMapper _mapper;
        public TendersController(IMapper mapper) => _mapper = mapper;

        /// <summary>
        /// Gets the list of tenders
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /tenders
        /// </remarks>
        /// <returns>Returns TendersListVm</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If user is unauthorized</response>
        [HttpGet]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<TendersListVm>> GetAll()
        {
            var query = new GetTenderListQuery();
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }

        /// <summary>
        /// Gets the tender by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /tenders/FF104BC5-FC71-465F-9A60-0C77B4B2CCC5
        /// </remarks>
        /// <param name="id">Tender id (guid)</param>
        /// <returns>Returns TenderDetailsVm</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If user is unauthorized</response>
        [HttpGet("{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<TenderDetailsVm>> Get(Guid id)
        {
            var query = new GetTenderDetailsQuery
            {
                TenderId = id
            };
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }

        /// <summary>
        /// Creates the tender
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// POST /tenders
        /// </remarks>
        /// <param name="createTenderDto">CreateTenderDto object</param>
        /// <returns>Returns id (guid)</returns>
        /// <response code="201">Success</response>
        /// <response code="401">If user is unauthorized</response>
        [HttpPost]
        [Authorize(Roles = "Client")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateTenderDto createTenderDto)
        {
            var command = _mapper.Map<CreateTenderCommand>(createTenderDto);
            var productId = await Mediator.Send(command);
            return Ok(productId);
        }

        /// <summary>
        /// Updates the tender
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// PUT /tenders/2F33AC0E-0458-4F1E-8C9F-E572392438C0/edit
        /// </remarks>
        /// <param name="updateTenderDto">UpdateTenderDto object</param>
        /// <param name="id">Tender id (Guid)</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="401">If user is unauthorized</response>
        [HttpPut("{id}/edit")]
        [Authorize(Roles = "Client")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> Update([FromBody] UpdateTenderDto updateTenderDto, Guid id)
        {
            var command = _mapper.Map<UpdateTenderCommand>(updateTenderDto);
            command.Id = id;
            await Mediator.Send(command);
            return NoContent();
        }

        /// <summary>
        /// Deletes the tender by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// DELETE /tenders/355D7383-D9BF-46AB-BCA6-2875AC746E2E/remove
        /// </remarks>
        /// <param name="id">Id of the tender (guid)</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="401">If user is unauthorized</response>
        [HttpDelete("{id}/remove")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> Delete(Guid id)
        {
            var command = new DeleteTenderCommand
            {
                TenderId = id
            };
            await Mediator.Send(command);
            return NoContent();
        }

        /// <summary>
        /// Registers the user in tender
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// DELETE /tenders/register
        /// </remarks>
        /// <param name="registerInTenderDto">RegisterInTenderDto object</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="401">If user is unauthorized</response>
        [HttpPost("register")]
        [Authorize(Roles = "Supplier")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> RegisterInTender([FromBody] RegisterInTenderDto registerInTenderDto)
        {
            var command = _mapper.Map<RegisterInTenderCommand>(registerInTenderDto);
            command.UserId = UserId;
            await Mediator.Send(command);
            return NoContent();
        }

        /// <summary>
        /// Closes the tender
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// DELETE /tenders/586BCDD5-91CF-4E05-B360-ACC8187FA4BB/executor/B37BFE17-802B-4C6C-B892-E44BE9BCB3A8
        /// </remarks>
        /// <param name="tenderId">Id of the tender</param>
        /// <param name="executorId">Id of the user</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="401">If user is unauthorized</response>
        [HttpPut("{tenderId}/executor/{executorId}")]
        [Authorize(Roles = "Client")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> CloseTender(Guid tenderId, Guid executorId)
        {
            var command = new CloseTenderCommand
            {
                TenderId = tenderId,
                ExecutorId = executorId
            };
            await Mediator.Send(command);
            return NoContent();
        }
    }
}
