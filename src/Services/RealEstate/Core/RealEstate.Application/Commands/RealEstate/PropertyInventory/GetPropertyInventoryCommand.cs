using RealEstate.Application.DTOs.RealEstate;
using RealEstate.Application.Interfaces.IRepository.RealEstate;
using MediatR;

namespace RealEstate.Application.Commands.RealEstate.PropertyInventory
{
    public class GetPropertyInventoryCommand : IRequest<PropertyInventoryDTO>
    {
        public Guid Id { get; set; }
    }
    public class GetPropertyInventoryCommandHandler(IPropertyInventoryRepository propertyInventoryRepository) : IRequestHandler<GetPropertyInventoryCommand, PropertyInventoryDTO>
    {
        private readonly IPropertyInventoryRepository _propertyInventoryRepository = propertyInventoryRepository;

        public async Task<PropertyInventoryDTO> Handle(GetPropertyInventoryCommand request, CancellationToken cancellationToken)
        {
            var model = await _propertyInventoryRepository.GetPropertyInventoryAsync(request.Id);
            return model;
        }
    }
}
