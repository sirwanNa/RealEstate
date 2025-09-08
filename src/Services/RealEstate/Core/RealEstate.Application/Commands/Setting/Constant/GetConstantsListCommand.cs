using RealEstate.Application.DTOs.Setting;
using RealEstate.Application.DTOs.Setting.Queries;
using RealEstate.Application.Interfaces.IRepository.Setting;
using MediatR;

namespace RealEstate.Application.Commands.Setting.Constant
{
    public class GetConstantsListCommand : IRequest<ConstantListDTO>
    {
        public required ConstantQueryDTO QueryModel { get; set; }
    }
    public class GetConstantsListCommandHandler(IConstantRepository constantRepository) : IRequestHandler<GetConstantsListCommand, ConstantListDTO>
    {
        private readonly IConstantRepository _constantRepository = constantRepository;

        public async Task<ConstantListDTO> Handle(GetConstantsListCommand request, CancellationToken cancellationToken)
        {
            var model = await _constantRepository.GetConstantsListAsync(request.QueryModel);
            return model;
        }
    }
}
