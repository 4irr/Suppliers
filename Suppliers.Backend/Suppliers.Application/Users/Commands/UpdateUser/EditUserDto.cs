using AutoMapper;
using Suppliers.Application.Common.Mappings;

namespace Suppliers.Application.Users.Commands.UpdateUser
{
    public class EditUserDto : IMapWith<UpdateUserCommand>
    {
        public string? Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int Age { get; set; }
        public string? Organization { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateUserCommand, EditUserDto>()
                .ForMember(dto => dto.Id,
                    opt => opt.MapFrom(command => command.Id))
                .ForMember(dto => dto.FirstName,
                    opt => opt.MapFrom(command => command.FirstName))
                .ForMember(dto => dto.LastName,
                    opt => opt.MapFrom(command => command.LastName))
                .ForMember(dto => dto.Age,
                    opt => opt.MapFrom(command => command.Age))
                .ForMember(dto => dto.Organization,
                    opt => opt.MapFrom(command => command.Organization));
        }
    }
}
