using AutoMapper;
using Suppliers.Application.Common.Mappings;
using Suppliers.Application.Orders.Commands.CreateOrder;

namespace Suppliers.WebApi.Models.Orders
{
    public class CreateOrderDto : IMapWith<CreateOrderCommand>
    {
        public Guid BatchId { get; set; }
        public Guid SupplierId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateOrderDto, CreateOrderCommand>()
                .ForMember(command => command.BatchId,
                    opt => opt.MapFrom(dto => dto.BatchId))
                .ForMember(command => command.SupplierId,
                    opt => opt.MapFrom(dto => dto.SupplierId));
        }
    }
}
