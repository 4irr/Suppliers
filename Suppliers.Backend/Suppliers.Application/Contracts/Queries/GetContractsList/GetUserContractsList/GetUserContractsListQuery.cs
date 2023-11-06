using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suppliers.Application.Contracts.Queries.GetContractsList.GetUserContractsList
{
    public class GetUserContractsListQuery : IRequest<ContractListVm>
    {
        public Guid UserId { get; set; }
    }
}
