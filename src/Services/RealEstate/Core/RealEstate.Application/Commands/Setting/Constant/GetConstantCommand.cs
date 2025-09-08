using RealEstate.Application.DTOs.Setting;
using RealEstate.Application.Interfaces.IRepository.Setting;
using MediatR;

namespace RealEstate.Application.Commands.Setting.Constant
{
    public class GetConstantCommand : IRequest<ConstanDTO>
    {
        public Guid Id { get; set; }
    }
    public class GetConstantCommandHandler(IConstantRepository constantRepository) : IRequestHandler<GetConstantCommand, ConstanDTO>
    {
        private readonly IConstantRepository _constantRepository = constantRepository;

        public async Task<ConstanDTO> Handle(GetConstantCommand request, CancellationToken cancellationToken)
        {
            var model = await _constantRepository.GetConstantAsync(request.Id);
            return model;
        }
    }
}
