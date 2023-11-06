using AutoMapper;
using Suppliers.Application.Common.Mappings;
using Suppliers.Domain;

namespace Suppliers.Application.Contracts.Queries.GetContractsList
{
    public class ContractLookupDto : IMapWith<Contract>
    {
        public Guid Id { get; set; }
        public Order Order { get; set; } = null!;
        public string? ConclusionDate { get; set; }
        public bool IsConfirmed { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Contract, ContractLookupDto>()
                .ForMember(dto => dto.Id,
                    opt => opt.MapFrom(contract => contract.Id))
                .ForMember(dto => dto.Order,
                    opt => opt.MapFrom(contract => contract.Order))
                .ForMember(dto => dto.ConclusionDate,
                    opt => opt.MapFrom(contract => contract.ConclusionDate.ToLongDateString()))
                .ForMember(dto => dto.IsConfirmed,
                    opt => opt.MapFrom(contract => contract.IsConfirmed));
        }
    }
}
