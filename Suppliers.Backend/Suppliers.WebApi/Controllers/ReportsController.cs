using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Suppliers.Application.Reports.Commands.CreateReport;
using Suppliers.Application.Reports.Commands.CreateTotalReport;
using Suppliers.WebApi.Models.Reports;

namespace Suppliers.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("/api/[controller]")]
    public class ReportsController : BaseController
    {
        private readonly IMapper _mapper;
        public ReportsController(IMapper mapper) => _mapper = mapper;

        /// <summary>
        /// Creates the single supplier trade report
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// POST /single
        /// </remarks>
        /// <returns>Returns SingleReportVm</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If user is unauthorized</response>
        [HttpPost("single")]
        [Authorize(Roles = "Supplier, Client")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> CreateSingleReport(CreateSingleReportDto createSingleReportDto)
        {
            var command = _mapper.Map<CreateSingleReportCommand>(createSingleReportDto);
            var vm = await Mediator.Send(command);

            return Ok(vm);
        }

        /// <summary>
        /// Creates the total trade report
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// POST /total
        /// </remarks>
        /// <returns>Returns TotalReportVm</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If user is unauthorized</response>
        [HttpPost("total")]
        [Authorize(Roles = "Client")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> CreateTotalReport(CreateTotalReportDto createTotalReportDto)
        {
            var command = _mapper.Map<CreateTotalReportCommand>(createTotalReportDto);
            var vm = await Mediator.Send(command);

            return Ok(vm);
        }
    }
}
