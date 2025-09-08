using RealEstate.Application.Interfaces.IRepository.Blog;
using MediatR;

namespace RealEstate.Application.Commands.Blog.Article
{
	public class DeleteArticleCommand : IRequest<int>
	{
		public Guid Id { get; set; }
	}
	public class DeleteArticleCommandHandler(IArticleRepository articleRepository) : IRequestHandler<DeleteArticleCommand, int>
	{
		private readonly IArticleRepository _articleRepository = articleRepository;
		public async Task<int> Handle(DeleteArticleCommand request, CancellationToken cancellationToken)
		{
			var result = await _articleRepository.DeleteArticleAsync(request.Id, cancellationToken);
			return result;
		}
	}
}
