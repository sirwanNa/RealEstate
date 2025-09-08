using RealEstate.Application.DTOs.RealEstate;
using RealEstate.Application.DTOs.RealEstate.Queries;
using RealEstate.Application.Interfaces.IRepository.RealEstate;
using MediatR;

namespace RealEstate.Application.Commands.RealEstate.PropertyInventory
{
    public class GetPropertyInventoriesListCommand : IRequest<PropertyInventoryListDTO>
    {
        public required PropertyInventoryQueryDTO QueryModel { get; set; }
    }
    public class GetPropertyInventoriesListCommandHandler(IPropertyInventoryRepository propertyInventoryRepository) : IRequestHandler<GetPropertyInventoriesListCommand, PropertyInventoryListDTO>
    {
        private readonly IPropertyInventoryRepository _propertyInventoryRepository = propertyInventoryRepository;

        public async Task<PropertyInventoryListDTO> Handle(GetPropertyInventoriesListCommand request, CancellationToken cancellationToken)
        {
            var model = await _propertyInventoryRepository.GetPropertyInventoriesListAsync(request.QueryModel);
            return model;
        }
    }
}
