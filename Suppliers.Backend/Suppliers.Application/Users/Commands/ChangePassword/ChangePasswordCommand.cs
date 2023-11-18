using AutoMapper;
using MediatR;
using Suppliers.Application.Common.Mappings;

namespace Suppliers.Application.Users.Commands.ChangePassword
{
    public class ChangePasswordCommand : IRequest, IMapWith<ChangePasswordDto>
    {
        public string? Id { get; set; }
        public string? OldPassword { get; set; }
        public string? NewPassword { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ChangePasswordCommand, ChangePasswordDto>()
                .ForMember(dto => dto.Id,
                    opt => opt.MapFrom(command => command.Id))
                .ForMember(dto => dto.OldPassword,
                    opt => opt.MapFrom(command => command.OldPassword))
                .ForMember(dto => dto.NewPassword,
                    opt => opt.MapFrom(command => command.NewPassword));
        }
    }
}
