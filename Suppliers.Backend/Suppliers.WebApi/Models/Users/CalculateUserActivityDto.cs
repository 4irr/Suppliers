using AutoMapper;
using Suppliers.Application.Common.Mappings;
using Suppliers.Application.Users.Commands.CalculateUserActivity;

namespace Suppliers.WebApi.Models.Users
{
    public class CalculateUserActivityDto : IMapWith<CalculateUserActivityCommand>
    {
        public Guid SupplierId { get; set; }
        public DateTime Beginning { get; set; }
        public DateTime Ending { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CalculateUserActivityDto, CalculateUserActivityCommand>()
                .ForMember(command => command.SupplierId,
                    opt => opt.MapFrom(dto => dto.SupplierId))
                .ForMember(command => command.Beginning,
                    opt => opt.MapFrom(dto => dto.Beginning))
                .ForMember(command => command.Ending,
                    opt => opt.MapFrom(dto => dto.Ending));
        }
    }
}
