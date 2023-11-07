using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Suppliers.Application.Contracts.Commands.ConfirmContract;
using Suppliers.Application.Contracts.Commands.CreateContract;
using Suppliers.Application.Contracts.Commands.DeleteContract;
using Suppliers.Application.Contracts.Queries.GetContractsList;
using Suppliers.Application.Contracts.Queries.GetContractsList.GetUserContractsList;
using Suppliers.WebApi.Models.Contracts;

namespace Suppliers.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("/api/[controller]")]
    public class ContractsController : BaseController
    {
        private readonly IMapper _mapper;
        public ContractsController(IMapper mapper) => _mapper = mapper;

        /// <summary>
        /// Gets the list of contracts
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /contracts
        /// </remarks>
        /// <returns>Returns ContractListVm</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If user is unauthorized</response>
        [HttpGet]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ContractListVm>> GetAll()
        {
            var query = new GetContractsListQuery();
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }

        /// <summary>
        /// Gets the list of contracts by user id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /contracts/user-contracts
        /// </remarks>
        /// <returns>Returns ContractListVm</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If user is unauthorized</response>
        [HttpGet("user-contracts")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ContractListVm>> GetAllByUserId()
        {
            var query = new GetUserContractsListQuery()
            {
                UserId = UserId
            };
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }

        /// <summary>
        /// Creates the contract
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// POST /contracts
        /// </remarks>
        /// <param name="createContractDto">CreateContractDto object</param>
        /// <returns>Returns id (guid)</returns>
        /// <response code="201">Success</response>
        /// <response code="401">If user is unauthorized</response>
        [HttpPost]
        [Authorize(Roles = "Client")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateContractDto createContractDto)
        {
            var command = _mapper.Map<CreateContractCommand>(createContractDto);
            var contractId = await Mediator.Send(command);
            return Ok(contractId);
        }

        /// <summary>
        /// Deletes the contract by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// DELETE /contracts/0F907076-13AF-4501-9AA3-3719D9DC7DF3/remove
        /// </remarks>
        /// <param name="id">Id of the contract (guid)</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="401">If user is unauthorized</response>
        [HttpDelete("{id}/remove")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> Delete(Guid id)
        {
            var command = new DeleteContractCommand
            {
                ContractId = id
            };
            await Mediator.Send(command);
            return NoContent();
        }

        /// <summary>
        /// Confirms the contract
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// POST /contracts/688DFE6E-8FEB-4E5A-A060-545465F58D87/confirm
        /// </remarks>
        /// <param name="id">contract id</param>
        /// <returns>NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="401">If user is unauthorized</response>
        [HttpPut("{id}/confirm")]
        [Authorize(Roles = "Supplier")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> ConfirmContract(Guid id)
        {
            var command = new ConfirmContractCommand
            {
                ContractId = id
            };
            await Mediator.Send(command);
            return NoContent();
        }
    }
}
