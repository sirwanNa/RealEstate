using RealEstate.Application.DTOs.Setting;
using RealEstate.Application.Interfaces.IRepository.Setting;
using MediatR;

namespace RealEstate.Application.Commands.Setting.Constant
{
    public class CreateConstantCommand : IRequest<int>
    {
        public required ConstanDTO Model { get; set; }
    }
    public class CreateConstantCommandHandler(IConstantRepository constantRepository) : IRequestHandler<CreateConstantCommand, int>
    {
        private readonly IConstantRepository _constantRepository = constantRepository;
        public async Task<int> Handle(CreateConstantCommand request, CancellationToken cancellationToken)
        {
            var result = await _constantRepository.CreateConstantAsync(request.Model, cancellationToken);
            return result;
        }
    }
}
