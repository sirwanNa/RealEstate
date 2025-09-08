using RealEstate.Application.DTOs.RealEstate;
using RealEstate.Application.Interfaces.IRepository.RealEstate;
using MediatR;
using Shared.Enums;

namespace RealEstate.Application.Commands.RealEstate.PropertyInventory
{
    public class GetPropertyInventoryEndUserCommand : IRequest<PropertyInventoryEndUserDTO>
    {
        public required string UrlTitle { get; set; }
        public required Language Language { get; set; }
        public int TotalOfRandomItems { get; set; } 
    }
    public class GetPropertyInventory_V2CommandHandler(IPropertyInventoryRepository propertyInventoryRepository) : IRequestHandler<GetPropertyInventoryEndUserCommand, PropertyInventoryEndUserDTO>
    {
        private readonly IPropertyInventoryRepository _propertyInventoryRepository = propertyInventoryRepository;

        public async Task<PropertyInventoryEndUserDTO> Handle(GetPropertyInventoryEndUserCommand request, CancellationToken cancellationToken)
        {
            var model = await _propertyInventoryRepository.GetPropertyInventoryAsync(request.UrlTitle, request.Language,request.TotalOfRandomItems);
            return model;
        }
    }
}
