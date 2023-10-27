using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Suppliers.Application.Products.Commands.CreateProduct;
using Suppliers.Application.Products.Commands.DeleteProduct;
using Suppliers.Application.Products.Commands.UpdateProduct;
using Suppliers.Application.Products.Queries.GetProductDetails;
using Suppliers.Application.Products.Queries.GetProductList;
using Suppliers.WebApi.Models;

namespace Suppliers.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("/api/[controller]")]
    public class ProductsController : BaseController
    {
        private readonly IMapper _mapper;
        public ProductsController(IMapper mapper) => _mapper = mapper;

        /// <summary>
        /// Gets the list of products
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /products
        /// </remarks>
        /// <returns>Returns ProductListVm</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If user is unauthorized</response>
        [HttpGet]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ProductListVm>> GetAll()
        {
            var query = new GetProductListQuery
            {
                UserId = UserId
            };
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }

        /// <summary>
        /// Gets the product by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /products/52CC20DE-DB78-4066-8098-BF778E5498F2
        /// </remarks>
        /// <param name="id">Product id (guid)</param>
        /// <returns>Returns ProductDetailsVm</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If user is unauthorized</response>
        [HttpGet("{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ProductDetailsVm>> Get(Guid id)
        {
            var query = new GetProductDetailsQuery
            {
                Id = id
            };
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }

        /// <summary>
        /// Creates the product
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// POST /products
        /// {
        ///     name: "product name",
        ///     price: "product price",
        ///     quantity: "product quantity",
        ///     expirationDate: "product expiration date"
        /// }
        /// </remarks>
        /// <param name="createProductDto">CreateProductDto object</param>
        /// <returns>Returns id (guid)</returns>
        /// <response code="201">Success</response>
        /// <response code="401">If user is unauthorized</response>
        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateProductDto createProductDto)
        {
            var command = _mapper.Map<CreateProductCommand>(createProductDto);
            command.UserId = UserId;
            var productId = await Mediator.Send(command);
            return Ok(productId);
        }

        /// <summary>
        /// Updates the product
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// PUT /products
        /// {
        ///     id: "product id",
        ///     name: "updated product name",
        ///     price: "updated product price",
        ///     quantity: "updated product quantity",
        ///     expirationDate: "updated product expiration date"
        /// }
        /// </remarks>
        /// <param name="updateProductDto">UpdateProductDto object</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="401">If user is unauthorized</response>
        [HttpPut]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> Update([FromBody] UpdateProductDto updateProductDto)
        {
            var command = _mapper.Map<UpdateProductCommand>(updateProductDto);
            command.UserId = UserId;
            await Mediator.Send(command);
            return NoContent();
        }

        /// <summary>
        /// Deletes the product by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// DELETE /products/087B7E28-D137-4346-BBC4-2BA733FDA3BC
        /// </remarks>
        /// <param name="id">Id of the product (guid)</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="401">If user is unauthorized</response>
        [HttpDelete("{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> Delete(Guid id)
        {
            var command = new DeleteProductCommand
            {
                Id = id,
                UserId = UserId
            };
            await Mediator.Send(command);
            return NoContent();
        }
    }
}
