using MediatR;
using Suppliers.Application.Interfaces;

namespace Suppliers.Application.Users.Commands.UnlockUser
{
    public class UnlockUserCommandHandler : IRequestHandler<UnlockUserCommand>
    {
        private readonly IUsersHttpClient _apiClient;

        public UnlockUserCommandHandler(IUsersHttpClient apiClient) => _apiClient = apiClient;

        public async Task Handle(UnlockUserCommand request, CancellationToken cancellationToken)
        {
            await _apiClient.UnlockUser(request.Id);
        }
    }
}
