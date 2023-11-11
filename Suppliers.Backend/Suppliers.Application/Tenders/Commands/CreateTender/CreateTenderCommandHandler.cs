using MediatR;
using Suppliers.Application.Common.Services;
using Suppliers.Application.Interfaces;
using Suppliers.Domain;

namespace Suppliers.Application.Tenders.Commands.CreateTender
{
    public class CreateTenderCommandHandler : IRequestHandler<CreateTenderCommand, Guid>
    {
        private readonly ISuppliersDbContext _context;
        private readonly EmailService _emailService;
        private readonly IUsersHttpClient _apiClient;

        public CreateTenderCommandHandler(ISuppliersDbContext context, EmailService emailService, IUsersHttpClient apiClient)
        {
            _context = context;
            _emailService = emailService;
            _apiClient = apiClient;
        }

        public async Task<Guid> Handle(CreateTenderCommand request, CancellationToken cancellationToken)
        {
            var tender = new Tender
            {
                Title = request.Title,
                Description = request.Description,
                Beginning = request.Beginning,
                Ending = request.Ending,
                IsOpen = true
            };

            await _context.Tenders.AddAsync(tender);
            await _context.SaveChangesAsync(cancellationToken);

            var users = await _apiClient.GetAllUsersAsync();

            foreach (var user in users)
            {
                if(user.EmailConfirmed)
                {
                    _emailService?.SendEmailAsync(user.Email, tender.Title!, 
                        $"<h3>На сайте появился новый тендер на закупку товаров!</h3><p>{tender.Description}</p>");
                }   
            }

            return tender.Id;
        }
    }
}
