using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Suppliers.Application.Suppliers.Queries.GetSupplierDetails;
using Suppliers.Application.Suppliers.Queries.GetSuppliersList;

namespace Suppliers.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("/api/[controller]")]
    public class UsersController : BaseController
    {
        /// <summary>
        /// Gets the list of suppliers
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /users/suppliers
        /// </remarks>
        /// <returns>Returns SuppliersListVm</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If user is unauthorized</response>
        [HttpGet("suppliers")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> GetSuppliersList()
        {
            var query = new GetSuppliersListQuery();
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }

        /// <summary>
        /// Gets the supplier by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /users/7F59F96F-ECB3-464D-89A0-714C37BCECE2
        /// </remarks>
        /// <param name="id">Supplier id (guid)</param>
        /// <returns>Returns SupplierDetailsVm</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If user is unauthorized</response>
        [HttpGet("{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<SupplierDetailsVm>> Get(Guid id)
        {
            var query = new GetSupplierDetailsQuery
            {
                Id = id
            };
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }

    }
}
