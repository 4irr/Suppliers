using AutoMapper;
using Suppliers.Application.Common.Mappings;
using Suppliers.Application.Contracts.Commands.CreateContract;

namespace Suppliers.WebApi.Models.Contracts
{
    public class CreateContractDto : IMapWith<CreateContractCommand>
    {
        public Guid OrderId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateContractDto, CreateContractCommand>()
                .ForMember(command => command.OrderId,
                    opt => opt.MapFrom(dto => dto.OrderId));
        }
    }
}
