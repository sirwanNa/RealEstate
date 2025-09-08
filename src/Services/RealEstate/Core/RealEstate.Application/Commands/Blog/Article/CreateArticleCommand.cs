using RealEstate.Application.DTOs.Blog;
using RealEstate.Application.Interfaces.IRepository.Blog;
using MediatR;

namespace RealEstate.Application.Commands.Blog.Article
{
	public class CreateArticleCommand : IRequest<int>
	{
		public required ArticleDTO Model { get; set; }
	}
	public class CreateArticleCommandHandler(IArticleRepository articleRepository) : IRequestHandler<CreateArticleCommand, int>
	{
		private readonly IArticleRepository _articleRepository = articleRepository;
		public async Task<int> Handle(CreateArticleCommand request, CancellationToken cancellationToken)
		{
			var result = await _articleRepository.CreateArticletAsync(request.Model, cancellationToken);
			return result;
		}
	}
}
