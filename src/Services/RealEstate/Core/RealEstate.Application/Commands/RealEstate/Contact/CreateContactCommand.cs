using RealEstate.Application.DTOs.RealEstate;
using RealEstate.Application.Interfaces.IRepository.RealEstate;
using MediatR;

namespace RealEstate.Application.Commands.RealEstate.Contact
{
	public class CreateContactCommand : IRequest<int>
	{
		public required ContactDTO Model { get; set; }
	}
	public class CreateContactCommandHandler(IContactRepository contactRepository) : IRequestHandler<CreateContactCommand, int>
	{
		private readonly IContactRepository _contactRepository = contactRepository;
		public async Task<int> Handle(CreateContactCommand request, CancellationToken cancellationToken)
		{
			var result = await _contactRepository.CreateContactAsync(request.Model, cancellationToken);
			return result;
		}
	}
}
