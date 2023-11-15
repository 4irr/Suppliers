using AutoMapper;
using Suppliers.Application.Common.Mappings;
using Suppliers.Domain;

namespace Suppliers.Application.Tenders.Queries.GetTendersList
{
    public class TenderLookupDto : IMapWith<Tender>
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Beginning { get; set; }
        public string? Ending { get; set; }
        public bool IsOpen { get; set; }
        public Guid ExecutorId { get; set; }
        public List<TenderUser>? tenderUsers { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Tender, TenderLookupDto>()
                .ForMember(dto => dto.Id,
                    opt => opt.MapFrom(tender => tender.Id))
                .ForMember(dto => dto.Title,
                    opt => opt.MapFrom(tender => tender.Title))
                .ForMember(dto => dto.Description,
                    opt => opt.MapFrom(tender => tender.Description))
                .ForMember(dto => dto.Beginning,
                    opt => opt.MapFrom(tender => tender.Beginning.Date.ToString()))
                .ForMember(dto => dto.Ending,
                    opt => opt.MapFrom(tender => tender.Ending.Date.ToString()))
                .ForMember(dto => dto.tenderUsers,
                    opt => opt.MapFrom(tender => tender.tenderUsers))
                .ForMember(dto => dto.IsOpen,
                    opt => opt.MapFrom(tender => tender.IsOpen))
                .ForMember(dto => dto.ExecutorId,
                    opt => opt.MapFrom(tender => tender.ExecutorId));
        }
    }
}
