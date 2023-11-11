using AutoMapper;
using Suppliers.Application.Common.Mappings;
using Suppliers.Application.Reports.Commands.CreateTotalReport;

namespace Suppliers.WebApi.Models.Reports
{
    public class CreateTotalReportDto : IMapWith<CreateTotalReportCommand>
    {
        public DateTime Beginning { get; set; }
        public DateTime Ending { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateTotalReportDto, CreateTotalReportCommand>()
                .ForMember(command => command.Beginning,
                    opt => opt.MapFrom(dto => dto.Beginning))
                .ForMember(command => command.Ending,
                    opt => opt.MapFrom(dto => dto.Ending));
        }
    }
}
