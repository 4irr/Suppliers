namespace Suppliers.Application.Contracts.Queries.GetContractsList
{
    public class ContractListVm
    {
        public IList<ContractLookupDto>? Contracts { get; set; }
    }
}
