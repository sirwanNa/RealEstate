using RealEstate.Application.DTOs.Common;
using RealEstate.Application.DTOs.RealEstate;
using RealEstate.Application.DTOs.RealEstate.Queries;
using Shared.Enums;

namespace RealEstate.Application.Interfaces.IRepository.RealEstate
{
	public interface IPropertyInventoryRepository
	{
		Task<PropertyInventoryDTO> GetPropertyInventoryAsync(Guid id);
		Task<PropertyInventoryEndUserDTO> GetPropertyInventoryAsync(string urlTitle, Language language, int totalOfRandomItems);
        Task<PropertyInventoryListDTO> GetPropertyInventoriesListAsync(PropertyInventoryQueryDTO queryModel);
		Task<int> CreatePropertyInventoryAsync(PropertyInventoryDTO model, CancellationToken cancellationToken);
		Task<int> CreatePropertyInventoryAsync(PropertyInventoryFromFilesDTO model, CancellationToken cancellationToken);
        Task<UploadFileResultDTO> UpdatePropertyInventoryAsync(PropertyInventoryDTO model, CancellationToken cancellationToken);
		Task<List<FileUploadViewModelPost>> DeletePropertyInventoryAsync(Guid id, CancellationToken cancellationToken);
	}
}
