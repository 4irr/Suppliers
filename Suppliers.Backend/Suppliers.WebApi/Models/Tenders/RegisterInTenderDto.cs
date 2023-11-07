using AutoMapper;
using Suppliers.Application.Common.Mappings;
using Suppliers.Application.Tenders.Commands.RegisterInTender;

namespace Suppliers.WebApi.Models.Tenders
{
    public class RegisterInTenderDto : IMapWith<RegisterInTenderCommand>
    {
        public Guid TenderId { get; set; }
        public string? UserDescription { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<RegisterInTenderDto, RegisterInTenderCommand>()
                .ForMember(command => command.TenderId,
                    opt => opt.MapFrom(dto => dto.TenderId))
                .ForMember(command => command.UserDescription,
                    opt => opt.MapFrom(dto => dto.UserDescription));
        }
    }
}
