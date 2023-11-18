using AutoMapper;
using MediatR;
using Suppliers.Application.Interfaces;

namespace Suppliers.Application.Users.Commands.UpdateUser
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand>
    {
        private readonly IMapper _mapper;
        private readonly IUsersHttpClient _apiClient;

        public UpdateUserCommandHandler(IMapper mapper, IUsersHttpClient apiClient) => 
            (_mapper, _apiClient) = (mapper, apiClient);

        public async Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var dto = _mapper.Map<EditUserDto>(request);

            await _apiClient.UpdateUser(dto);
        }
    }
}
