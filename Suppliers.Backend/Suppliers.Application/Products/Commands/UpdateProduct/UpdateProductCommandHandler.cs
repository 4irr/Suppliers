using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Suppliers.Application.Common.Exceptions;
using Suppliers.Application.Common.Services;
using Suppliers.Application.Interfaces;
using Suppliers.Application.Suppliers.Queries.GetSuppliersList;
using Suppliers.Domain;

namespace Suppliers.Application.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand>
    {
        private readonly ISuppliersDbContext _context;
        private readonly EmailService _emailService;
        private readonly IConfiguration _configuration;
        private readonly IUsersHttpClient _apiClient;

        public UpdateProductCommandHandler(ISuppliersDbContext context, EmailService emailService, 
            IConfiguration configuration, IUsersHttpClient apiClient)
        {
            _context = context;
            _emailService = emailService;
            _configuration = configuration;
            _apiClient = apiClient;
        }

        public async Task Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Products.FirstOrDefaultAsync(product => product.Id == request.Id, cancellationToken);

            if (entity == null || entity.UserId != request.UserId) 
            {
                throw new NotFoundException(nameof(Product), request.Id);
            }

            if (entity.Price != request.Price) 
            {
                var user = await _apiClient.GetUserByIdAsync(request.UserId);

                var contract = await _context.Contracts.FirstOrDefaultAsync(contract =>
                    contract.Order.Batch.ProductId == request.Id &&
                    contract.Order.SupplierId == request.UserId, cancellationToken);

                if (user == null) 
                {
                    throw new NotFoundException(nameof(AppUserDto), request.UserId);
                }

                if(contract != null)
                    await _emailService.SendEmailAsync(_configuration.GetSection("ClientEmail").Value, "Поставщик изменил цену",
                        "<h3>Поставщик, с которым вы торговали, изменил цену на свой товар!</h3>" +
                        $"<p>Поставщик {user.Organization} изменил цену на товар {request.Name} с {entity.Price} р. на {request.Price} р.</p>");
            }

            entity.Name = request.Name;
            entity.Price = request.Price;
            entity.Quantity = request.Quantity;
            entity.ExpirationDate = request.ExpirationDate;

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
