using RealEstate.Application.DTOs.RealEstate;
using RealEstate.Application.Interfaces.IRepository.RealEstate;
using MediatR;

namespace RealEstate.Application.Commands.RealEstate.Contact
{
	public class GetContactCommand : IRequest<ContactDTO>
	{
		public Guid Id { get; set; }
	}
	public class GetContactCommandCommandHandler(IContactRepository contactRepository) : IRequestHandler<GetContactCommand, ContactDTO>
	{
		private readonly IContactRepository _contactRepository = contactRepository;

		public async Task<ContactDTO> Handle(GetContactCommand request, CancellationToken cancellationToken)
		{
			var model = await _contactRepository.GetContactAsync(request.Id);
			return model;
		}
	}
}
