using AutoMapper;
using Suppliers.Application.Common.Mappings;
using Suppliers.Application.Suppliers.Queries.GetSuppliersList;

namespace Suppliers.Application.Suppliers.Queries.GetSupplierDetails
{
    public class UserDetailsVm : IMapWith<AppUserDto>
    {
        public string? Id { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public int Age { get; set; }
        public string Email { get; set; } = null!;
        public string Role { get; set; } = null!;
        public string? Organization { get; set; }
        public bool IsLicenseLoaded { get; set; }
        public bool IsLicensed { get; set; }
        public string? LicensePath { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<AppUserDto, UserDetailsVm>()
                .ForMember(vm => vm.Id,
                    opt => opt.MapFrom(dto => dto.Id))
                .ForMember(vm => vm.FirstName,
                    opt => opt.MapFrom(dto => dto.FirstName))
                .ForMember(vm => vm.LastName,
                    opt => opt.MapFrom(dto => dto.LastName))
                .ForMember(vm => vm.Age,
                    opt => opt.MapFrom(dto => dto.Age))
                .ForMember(vm => vm.Email,
                    opt => opt.MapFrom(dto => dto.Email))
                .ForMember(vm => vm.Role,
                    opt => opt.MapFrom(dto => dto.Role))
                .ForMember(vm => vm.Organization,
                    opt => opt.MapFrom(dto => dto.Organization))
                .ForMember(vm => vm.IsLicenseLoaded,
                    opt => opt.MapFrom(dto => dto.IsLicenseLoaded))
                .ForMember(vm => vm.IsLicensed,
                    opt => opt.MapFrom(dto => dto.IsLicensed))
                .ForMember(vm => vm.LicensePath,
                    opt => opt.MapFrom(dto => dto.LicensePath));
        }
    }
}
