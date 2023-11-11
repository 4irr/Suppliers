using AutoMapper;
using Suppliers.Application.Common.Mappings;
using Suppliers.Domain;

namespace Suppliers.Application.Reports.Commands.CreateReport
{
    public class ContractLookupDto : IMapWith<Contract>
    {
        public Guid Id { get; set; }
        public string? ConclusionDate { get; set; }
        public float OrderPrice { get; set; }
        public Product Product { get; set; } = null!;
        public int Quantity { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Contract, ContractLookupDto>()
                .ForMember(dto => dto.Id,
                    opt => opt.MapFrom(contract => contract.Id))
                .ForMember(dto => dto.ConclusionDate,
                    opt => opt.MapFrom(contract => contract.ConclusionDate.ToLongDateString()))
                .ForMember(dto => dto.OrderPrice,
                    opt => opt.MapFrom(contract => contract.Order.OrderPrice))
                .ForMember(dto => dto.Product,
                    opt => opt.MapFrom(contract => contract.Order.Batch.Product))
                .ForMember(dto => dto.Quantity,
                    opt => opt.MapFrom(contract => contract.Order.Batch.Quantity));
        }
    }
}
