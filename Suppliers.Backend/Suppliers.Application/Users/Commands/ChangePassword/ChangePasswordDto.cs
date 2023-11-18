using AutoMapper;
using Suppliers.Application.Common.Mappings;

namespace Suppliers.Application.Users.Commands.ChangePassword
{
    public class ChangePasswordDto : IMapWith<ChangePasswordCommand>
    {
        public string? Id { get; set; }
        public string? OldPassword { get; set; }
        public string? NewPassword { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ChangePasswordDto, ChangePasswordCommand>()
                .ForMember(command => command.Id,
                    opt => opt.MapFrom(dto => dto.Id))
                .ForMember(command => command.OldPassword,
                    opt => opt.MapFrom(dto => dto.OldPassword))
                .ForMember(command => command.NewPassword,
                    opt => opt.MapFrom(dto => dto.NewPassword));
        }
    }
}
