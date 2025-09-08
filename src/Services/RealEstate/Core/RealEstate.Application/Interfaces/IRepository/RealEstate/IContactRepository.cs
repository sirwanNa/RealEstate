using RealEstate.Application.DTOs.RealEstate;
using RealEstate.Application.DTOs.RealEstate.Queries;

namespace RealEstate.Application.Interfaces.IRepository.RealEstate
{
	public interface IContactRepository
	{
		Task<ContactDTO> GetContactAsync(Guid id);
		Task<ContactListDTO> GetContactsList(ContactQueryDTO queryModel);
		Task<int> CreateContactAsync(ContactDTO model, CancellationToken cancellationToken);
		Task<int> DeleteContactAsync(Guid id, CancellationToken cancellationToken);
	}
}
