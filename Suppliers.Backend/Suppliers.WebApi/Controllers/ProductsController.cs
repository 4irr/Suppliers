using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Suppliers.Application.Products.Commands.CreateProduct;
using Suppliers.Application.Products.Commands.DeleteProduct;
using Suppliers.Application.Products.Commands.UpdateProduct;
using Suppliers.Application.Products.Queries.GetProductDetails;
using Suppliers.Application.Products.Queries.GetProductList;
using Suppliers.WebApi.Models;

namespace Suppliers.WebApi.Controllers
{
    [Route("/api/[controller]")]
    public class ProductsController : BaseController
    {
        private readonly IMapper _mapper;
        public ProductsController(IMapper mapper) => _mapper = mapper;

        [HttpGet]
        public async Task<ActionResult<ProductListVm>> GetAll()
        {
            var query = new GetProductListQuery();
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDetailsVm>> Get(Guid id)
        {
            var query = new GetProductDetailsQuery
            {
                Id = id
            };
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateProductDto createProductDto)
        {
            var command = _mapper.Map<CreateProductCommand>(createProductDto);
            command.UserId = UserId;
            var productId = await Mediator.Send(command);
            return Ok(productId);
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromBody] UpdateProductDto updateProductDto)
        {
            var command = _mapper.Map<UpdateProductCommand>(updateProductDto);
            command.UserId = UserId;
            await Mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
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
