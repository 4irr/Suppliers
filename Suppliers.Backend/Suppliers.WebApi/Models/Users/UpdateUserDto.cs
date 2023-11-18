using AutoMapper;
using Suppliers.Application.Common.Mappings;
using Suppliers.Application.Users.Commands.UpdateUser;

namespace Suppliers.WebApi.Models.Users
{
    public class UpdateUserDto : IMapWith<UpdateUserCommand>
    {
        public string? Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int Age { get; set; }
        public string? Organization { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateUserDto, UpdateUserCommand>()
                .ForMember(command => command.Id,
                    opt => opt.MapFrom(dto => dto.Id))
                .ForMember(command => command.FirstName,
                    opt => opt.MapFrom(dto => dto.FirstName))
                .ForMember(command => command.LastName,
                    opt => opt.MapFrom(dto => dto.LastName))
                .ForMember(command => command.Age,
                    opt => opt.MapFrom(dto => dto.Age))
                .ForMember(command => command.Organization,
                    opt => opt.MapFrom(dto => dto.Organization));
        }
    }
}
