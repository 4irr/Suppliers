using AutoMapper;
using MediatR;
using Suppliers.Application.Interfaces;

namespace Suppliers.Application.Users.Commands.ChangePassword
{
    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand>
    {
        private readonly IMapper _mapper;
        private readonly IUsersHttpClient _apiClient;

        public ChangePasswordCommandHandler(IMapper mapper, IUsersHttpClient apiClient) =>
            (_mapper, _apiClient) = (mapper, apiClient);

        public async Task Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var dto = _mapper.Map<ChangePasswordDto>(request);

            await _apiClient.ChangePassword(dto);
        }
    }
}
