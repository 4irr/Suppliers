using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suppliers.Application.Contracts.Commands.CreateContract
{
    public class CreateContractCommand : IRequest<Guid>
    {
        public Guid OrderId { get; set; }
    }
}
