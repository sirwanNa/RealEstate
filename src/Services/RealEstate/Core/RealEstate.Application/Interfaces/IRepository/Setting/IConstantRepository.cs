using RealEstate.Application.DTOs.Setting;
using RealEstate.Application.DTOs.Setting.Queries;

namespace RealEstate.Application.Interfaces.IRepository.Setting
{
	public interface IConstantRepository 
    {
        Task<ConstanDTO> GetConstantAsync(Guid id);
        Task<ConstantListDTO> GetConstantsListAsync(ConstantQueryDTO queryModel);
        Task<int> CreateConstantAsync(ConstanDTO model, CancellationToken cancellationToken);
        Task<int> UpdateConstantAsync(ConstanDTO model, CancellationToken cancellationToken);
        Task<int> DeleteConstantAsync(Guid id, CancellationToken cancellationToken);
    }
}
