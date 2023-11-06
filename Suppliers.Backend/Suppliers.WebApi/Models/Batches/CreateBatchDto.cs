using AutoMapper;
using Suppliers.Application.Batches.Commands.CreateBatch;
using Suppliers.Application.Common.Mappings;

namespace Suppliers.WebApi.Models.Batches
{
    public class CreateBatchDto : IMapWith<CreateBatchCommand>
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateBatchDto, CreateBatchCommand>()
                .ForMember(command => command.ProductId,
                    opt => opt.MapFrom(dto => dto.ProductId))
                .ForMember(command => command.Quantity,
                    opt => opt.MapFrom(dto => dto.Quantity));
        }
    }
}
