using RealEstate.Application.DTOs.Setting;
using RealEstate.Application.Interfaces.IRepository.Setting;
using MediatR;

namespace RealEstate.Application.Commands.Setting.Constant
{
    public class UpdateConstantCommand : IRequest<int>
    {
        public required ConstanDTO Model { get; set; }
    }
    public class UpdateConstantCommandHandler(IConstantRepository constantRepository) : IRequestHandler<UpdateConstantCommand, int>
    {
        private readonly IConstantRepository _constantRepository = constantRepository;
        public async Task<int> Handle(UpdateConstantCommand request, CancellationToken cancellationToken)
        {
            var result = await _constantRepository.UpdateConstantAsync(request.Model, cancellationToken);
            return result;
        }
    }
}
