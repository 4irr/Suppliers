using AutoMapper;
using Suppliers.Application.Common.Mappings;
using Suppliers.Domain;

namespace Suppliers.Application.Users.Commands.CalculateUserActivity
{
    public class TenderUserLookupDto : IMapWith<TenderUser>
    {
        public Guid TenderId { get; set; }
        public Tender? Tender { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<TenderUser, TenderUserLookupDto>()
                .ForMember(dto => dto.TenderId,
                    opt => opt.MapFrom(tender => tender.TenderId))
                .ForMember(dto => dto.Tender,
                    opt => opt.MapFrom(tender => tender.Tender));
        }
    }
}
