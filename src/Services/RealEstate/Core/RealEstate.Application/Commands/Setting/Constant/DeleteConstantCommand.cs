using RealEstate.Application.Interfaces.IRepository.Setting;
using MediatR;

namespace RealEstate.Application.Commands.Setting.Constant
{
    public class DeleteConstantCommand : IRequest<int>
    {
        public Guid Id { get; set; }
    }
    public class DeleteConstantCommandHandler(IConstantRepository constantRepository) : IRequestHandler<DeleteConstantCommand, int>
    {
        private readonly IConstantRepository _constantRepository = constantRepository;
        public async Task<int> Handle(DeleteConstantCommand request, CancellationToken cancellationToken)
        {
            var result = await _constantRepository.DeleteConstantAsync(request.Id, cancellationToken);
            return result;
        }
    }
}
