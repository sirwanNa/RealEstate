using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealEstate.Application.DTOs.RealEstate;
using RealEstate.Application.Interfaces.IRepository.RealEstate;
using MediatR;

namespace RealEstate.Application.Commands.RealEstate.PropertyInventory
{
    public class CreatePropertyInventoryFromFilesCommand : IRequest<int>
    {
        public required PropertyInventoryFromFilesDTO Model { get; set; }
    }
    public class CreatePropertyInventoryFromFilesCommandHandler(IPropertyInventoryRepository propertyInventoryRepository) : IRequestHandler<CreatePropertyInventoryFromFilesCommand, int>
    {
        private readonly IPropertyInventoryRepository _propertyInventoryRepository = propertyInventoryRepository;
        public async Task<int> Handle(CreatePropertyInventoryFromFilesCommand request, CancellationToken cancellationToken)
        {
            var result = await _propertyInventoryRepository.CreatePropertyInventoryAsync(request.Model, cancellationToken);
            return result;
        }
    }
}
