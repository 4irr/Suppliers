using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
    }
}
