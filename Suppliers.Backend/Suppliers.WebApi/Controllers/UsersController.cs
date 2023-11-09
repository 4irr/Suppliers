using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.FileProviders;
using Suppliers.Application.Suppliers.Commands.ConfirmLicense;
using Suppliers.Application.Suppliers.Commands.LoadLicense;
using Suppliers.Application.Suppliers.Queries.GetSupplierDetails;
using Suppliers.Application.Suppliers.Queries.GetSuppliersList;
using System.IO;

namespace Suppliers.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("/api/[controller]")]
    public class UsersController : BaseController
    {
        private readonly IWebHostEnvironment _appEnvironment;

        public UsersController(IWebHostEnvironment appEnvironment) => _appEnvironment = appEnvironment;

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
        [Authorize(Roles = "Client, Admin")]
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

        /// <summary>
        /// Saves suppliers license
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /users/load-license
        /// </remarks>
        /// <param name="formFile">Suppliers license</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="401">If user is unauthorized</response>
        [HttpPost("save-license")]
        [Authorize(Roles = "Supplier")]
        [Consumes("multipart/form-data")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> SaveLicense([FromForm] IFormFile formFile)
        {
            var command = new SaveLicenseCommand
            {
                UserId = UserId,
                FormFile = formFile,
                FullPath = _appEnvironment.WebRootPath + "/Licenses/" + formFile.FileName
            };
            await Mediator.Send(command);

            return NoContent();
        }

        [HttpGet("{id}/load-license")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> LoadLicense(Guid id)
        {
            var command = new LoadLicenseCommand
            {
                UserId = id
            };
            var dto = await Mediator.Send(command);
            return File(dto.FileStream!, dto.ContentType!, dto.FileName!);
        }

        [HttpPost("{id}/confirm-license")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> ConfirmLicense(Guid id)
        {
            var command = new ConfirmLicenseCommand
            {
                UserId = id
            };
            await Mediator.Send(command);
            return NoContent();
        }
    }
}
