using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Suppliers.Application.Suppliers.Commands.ConfirmLicense;
using Suppliers.Application.Suppliers.Commands.LoadLicense;
using Suppliers.Application.Suppliers.Queries.GetSupplierDetails;
using Suppliers.Application.Suppliers.Queries.GetSuppliersList;
using Suppliers.Application.Users.Commands.ChangePassword;
using Suppliers.Application.Users.Commands.ConfirmRegister;
using Suppliers.Application.Users.Commands.UpdateUser;
using Suppliers.WebApi.Models.Users;

namespace Suppliers.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("/api/[controller]")]
    public class UsersController : BaseController
    {
        private readonly IWebHostEnvironment _appEnvironment;
        private readonly IMapper _mapper;

        public UsersController(IWebHostEnvironment appEnvironment, IMapper mapper) => 
            (_appEnvironment, _mapper) = (appEnvironment, mapper);

        /// <summary>
        /// Gets the list of users
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /users
        /// </remarks>
        /// <returns>Returns UsersListVm</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If user is unauthorized</response>
        [HttpGet]
        [Authorize(Roles = "Client, Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> GetUsersList()
        {
            var query = new GetUsersListQuery();

            var vm = await Mediator.Send(query);

            return Ok(vm);
        }

        /// <summary>
        /// Gets the user by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /users/7F59F96F-ECB3-464D-89A0-714C37BCECE2
        /// </remarks>
        /// <param name="id">User id (guid)</param>
        /// <returns>Returns UserDetailsVm</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If user is unauthorized</response>
        [HttpGet("{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<UserDetailsVm>> Get(Guid id)
        {
            var query = new GetUsersDetailsQuery
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
        /// GET /users/save-license
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

        /// <summary>
        /// Loads suppliers license
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /users/0F9AAB30-64CD-401F-950A-AF97DE1A2499/load-license
        /// </remarks>
        /// <param name="id">Supplier id (Guid)</param>
        /// <returns>Returns File</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If user is unauthorized</response>
        [HttpGet("{id}/load-license")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> LoadLicense(Guid id)
        {
            var command = new LoadLicenseCommand
            {
                UserId = id
            };

            var dto = await Mediator.Send(command);

            return File(dto.FileStream!, dto.ContentType!, dto.FileName!);
        }

        /// <summary>
        /// Confirms suppliers license
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /users/E9E590B4-1F64-4E54-834B-7D6D4BAC80B7/load-license
        /// </remarks>
        /// <param name="id">Supplier id (Guid)</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="401">If user is unauthorized</response>
        [HttpPost("{id}/confirm-license")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> ConfirmLicense(Guid id)
        {
            var command = new ConfirmLicenseCommand
            {
                UserId = id
            };

            await Mediator.Send(command);

            return NoContent();
        }

        /// <summary>
        /// Updates the user
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// PUT /users
        /// </remarks>
        /// <param name="dto">UpdateUserDto object</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="401">If user is unauthorized</response>
        [HttpPut]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> UpdateUser(UpdateUserDto dto)
        {
            var command = _mapper.Map<UpdateUserCommand>(dto);
            
            await Mediator.Send(command);

            return NoContent();
        }

        /// <summary>
        /// Changes the users password
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// PUT /users
        /// </remarks>
        /// <param name="dto">ChangeUserDto object</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="401">If user is unauthorized</response>
        [HttpPut("password")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> ChangePassword(ChangePasswordDto dto)
        {
            var command = _mapper.Map<ChangePasswordCommand>(dto);

            await Mediator.Send(command);

            return NoContent();
        }

        /// <summary>
        /// Confirmes supplier registration
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// PUT /users/80094940-290E-4985-AE7C-9DCD85AED5BE/register/confirm
        /// </remarks>
        /// <param name="id">Supplier id (guid)</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="401">If user is unauthorized</response>
        [HttpPut("{id}/register/confirm")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> ConfirmRegister(Guid id)
        {
            var command = new ConfirmRegisterCommand
            {
                Id = id
            };

            await Mediator.Send(command);

            return NoContent();
        }
    }
}
