using RealEstate.Application.Commands.RealEstate.PropertyInventory;
using RealEstate.Application.DTOs.RealEstate;
using RealEstate.Application.DTOs.RealEstate.Queries;
using RealEstate.Application.Interfaces.IRepository.RealEstate;
using MediatR;

namespace RealEstate.Application.Commands.RealEstate.Contact
{
	public class GetContactsListCommand : IRequest<ContactListDTO>
	{
		public required ContactQueryDTO QueryModel { get; set; }
	}
	public class GetContactsListCommandHandler(IContactRepository contactRepository) : IRequestHandler<GetContactsListCommand, ContactListDTO>
	{
		private readonly IContactRepository _contactRepository = contactRepository;

		public async Task<ContactListDTO> Handle(GetContactsListCommand request, CancellationToken cancellationToken)
		{
			var model = await _contactRepository.GetContactsList(request.QueryModel);
			return model;
		}
	}
}
