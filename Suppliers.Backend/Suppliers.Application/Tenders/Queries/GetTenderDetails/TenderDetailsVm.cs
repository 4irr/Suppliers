using AutoMapper;
using Suppliers.Application.Common.Mappings;
using Suppliers.Domain;

namespace Suppliers.Application.Tenders.Queries.GetTenderDetails
{
    public class TenderDetailsVm : IMapWith<Tender>
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Beginning { get; set; }
        public string? Ending { get; set; }
        public bool IsOpen { get; set; }
        public Guid ExecutorId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Tender, TenderDetailsVm>()
                .ForMember(vm => vm.Id,
                    opt => opt.MapFrom(tender => tender.Id))
                .ForMember(vm => vm.Title,
                    opt => opt.MapFrom(tender => tender.Title))
                .ForMember(vm => vm.Description,
                    opt => opt.MapFrom(tender => tender.Description))
                .ForMember(vm => vm.Beginning,
                    opt => opt.MapFrom(tender => tender.Beginning.ToString("yyyy-MM-dd")))
                .ForMember(vm => vm.Ending,
                    opt => opt.MapFrom(tender => tender.Ending.ToString("yyyy-MM-dd")))
                .ForMember(vm => vm.IsOpen,
                    opt => opt.MapFrom(tender => tender.IsOpen))
                .ForMember(vm => vm.ExecutorId,
                    opt => opt.MapFrom(tender => tender.ExecutorId));
        }
    }
}
