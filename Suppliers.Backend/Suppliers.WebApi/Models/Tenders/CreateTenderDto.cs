using AutoMapper;
using Suppliers.Application.Common.Mappings;
using Suppliers.Application.Tenders.Commands.CreateTender;

namespace Suppliers.WebApi.Models.Tenders
{
    public class CreateTenderDto : IMapWith<CreateTenderCommand>
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime Beginning { get; set; }
        public DateTime Ending { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateTenderDto, CreateTenderCommand>()
                .ForMember(command => command.Title,
                    opt => opt.MapFrom(dto => dto.Title))
                .ForMember(command => command.Description,
                    opt => opt.MapFrom(dto => dto.Description))
                .ForMember(command => command.Beginning,
                    opt => opt.MapFrom(dto => dto.Beginning))
                .ForMember(command => command.Ending,
                    opt => opt.MapFrom(dto => dto.Ending));
        }
    }
}
