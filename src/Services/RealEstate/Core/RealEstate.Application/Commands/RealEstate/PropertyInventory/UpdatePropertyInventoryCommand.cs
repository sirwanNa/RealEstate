using RealEstate.Application.DTOs.RealEstate;
using RealEstate.Application.Interfaces.IRepository.RealEstate;
using MediatR;

namespace RealEstate.Application.Commands.RealEstate.PropertyInventory
{
    public class UpdatePropertyInventoryCommand : IRequest<UploadFileResultDTO>
    {
        public required PropertyInventoryDTO Model { get; set; }
    }
    public class UpdatePropertyInventoryCommandHandler(IPropertyInventoryRepository propertyInventoryRepository) : IRequestHandler<UpdatePropertyInventoryCommand, UploadFileResultDTO>
    {
        private readonly IPropertyInventoryRepository _propertyInventoryRepository = propertyInventoryRepository;
        public async Task<UploadFileResultDTO> Handle(UpdatePropertyInventoryCommand request, CancellationToken cancellationToken)
        {
            var result = await _propertyInventoryRepository.UpdatePropertyInventoryAsync(request.Model, cancellationToken);
            return result;
        }
    }
}
