using MediatR;
using Suppliers.Application.Interfaces;

namespace Suppliers.Application.Users.Commands.ConfirmRegister
{
    public class ConfirmRegisterCommandHandler : IRequestHandler<ConfirmRegisterCommand>
    {
        private IUsersHttpClient _apiClient;

        public ConfirmRegisterCommandHandler(IUsersHttpClient apiClient) => _apiClient = apiClient;

        public async Task Handle(ConfirmRegisterCommand request, CancellationToken cancellationToken)
        {
            await _apiClient.ConfirmRegister(request.Id);
        }
    }
}
