using MediatR;
using Suppliers.Application.Interfaces;

namespace Suppliers.Application.Users.Commands.BlockUser
{
    public class BlockUserCommandHandler : IRequestHandler<BlockUserCommand>
    {
        private readonly IUsersHttpClient _apiClient;

        public BlockUserCommandHandler(IUsersHttpClient apiClient) => _apiClient = apiClient;

        public async Task Handle(BlockUserCommand request, CancellationToken cancellationToken)
        {
            await _apiClient.BlockUser(request.Id);
        }
    }
}
