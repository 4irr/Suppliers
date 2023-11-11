using AutoMapper;
using Suppliers.Application.Common.Mappings;
using Suppliers.Application.Reports.Commands.CreateReport;

namespace Suppliers.WebApi.Models.Reports
{
    public class CreateSingleReportDto : IMapWith<CreateSingleReportCommand>
    {
        public Guid SupplierId { get; set; }
        public DateTime Beginning { get; set; }
        public DateTime Ending { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateSingleReportDto, CreateSingleReportCommand>()
                .ForMember(command => command.SupplierId,
                    opt => opt.MapFrom(dto => dto.SupplierId))
                .ForMember(command => command.Beginning,
                    opt => opt.MapFrom(dto => dto.Beginning))
                .ForMember(command => command.Ending,
                    opt => opt.MapFrom(dto => dto.Ending));
        }
    }
}
