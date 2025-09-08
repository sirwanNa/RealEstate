using RealEstate.Application.Interfaces.IRepository.RealEstate;
using MediatR;

namespace RealEstate.Application.Commands.RealEstate.Contact
{
	public class DeleteContactCommand : IRequest<int>
	{
		public Guid Id { get; set; }
	}
	public class DeleteContactCommandHandler(IContactRepository contactRepository) : IRequestHandler<DeleteContactCommand, int>
	{
		private readonly IContactRepository _contactRepository = contactRepository;
		public async Task<int> Handle(DeleteContactCommand request, CancellationToken cancellationToken)
		{
			var result = await _contactRepository.DeleteContactAsync(request.Id, cancellationToken);
			return result;
		}
	}
}
