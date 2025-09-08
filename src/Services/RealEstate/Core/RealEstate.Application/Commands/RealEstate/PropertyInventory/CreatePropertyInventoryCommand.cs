using RealEstate.Application.DTOs.RealEstate;
using RealEstate.Application.Interfaces.IRepository.RealEstate;
using MediatR;

namespace RealEstate.Application.Commands.RealEstate.PropertyInventory
{
    public class CreatePropertyInventoryCommand : IRequest<int>
    {
        public required PropertyInventoryDTO Model { get; set; }
    }
    public class CreatePropertyInventoryCommandHandler(IPropertyInventoryRepository propertyInventoryRepository) : IRequestHandler<CreatePropertyInventoryCommand, int>
    {
        private readonly IPropertyInventoryRepository _propertyInventoryRepository = propertyInventoryRepository;
        public async Task<int> Handle(CreatePropertyInventoryCommand request, CancellationToken cancellationToken)
        {
            var result = await _propertyInventoryRepository.CreatePropertyInventoryAsync(request.Model, cancellationToken);
            return result;
        }
    }
}
