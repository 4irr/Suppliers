using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Suppliers.Application.Orders.Commands.CreateOrder;
using Suppliers.Application.Orders.Commands.DeleteOrder;
using Suppliers.WebApi.Models.Orders;

namespace Suppliers.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("/api/[controller]")]
    public class OrdersController : BaseController
    {
        private readonly IMapper _mapper;
        public OrdersController(IMapper mapper) => _mapper = mapper;

        /// <summary>
        /// Creates the order
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// POST /orders
        /// </remarks>
        /// <param name="createOrderDto">CreateOrderDto object</param>
        /// <returns>Returns id (guid)</returns>
        /// <response code="201">Success</response>
        /// <response code="401">If user is unauthorized</response>
        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateOrderDto createOrderDto)
        {
            var command = _mapper.Map<CreateOrderCommand>(createOrderDto);
            var orderId = await Mediator.Send(command);
            return Ok(orderId);
        }

        /// <summary>
        /// Deletes the order by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// DELETE /orders/AAB917A8-4085-486E-8E34-58B290269BCC/remove
        /// </remarks>
        /// <param name="id">Id of the order (guid)</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="401">If user is unauthorized</response>
        [HttpDelete("{id}/remove")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> Delete(Guid id)
        {
            var command = new DeleteOrderCommand
            {
                OrderId = id
            };
            await Mediator.Send(command);
            return NoContent();
        }
    }
}
