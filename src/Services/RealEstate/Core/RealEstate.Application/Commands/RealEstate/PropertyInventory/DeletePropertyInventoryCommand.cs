using RealEstate.Application.DTOs.Common;
using RealEstate.Application.Interfaces.IRepository.RealEstate;
using MediatR;

namespace RealEstate.Application.Commands.RealEstate.PropertyInventory
{
    public class DeletePropertyInventoryCommand : IRequest<List<FileUploadViewModelPost>>
    {
        public Guid Id { get; set; }
    }
    public class DeletePropertyInventoryCommandHandler(IPropertyInventoryRepository propertyInventoryRepository) : IRequestHandler<DeletePropertyInventoryCommand, List<FileUploadViewModelPost>>
    {
        private readonly IPropertyInventoryRepository _propertyInventoryRepository = propertyInventoryRepository;
        public async Task<List<FileUploadViewModelPost>> Handle(DeletePropertyInventoryCommand request, CancellationToken cancellationToken)
        {
            var result = await _propertyInventoryRepository.DeletePropertyInventoryAsync(request.Id, cancellationToken);
            return result;
        }
    }
}
