using RealEstate.Application.DTOs.Blog;
using RealEstate.Application.Interfaces.IRepository.Blog;
using MediatR;

namespace RealEstate.Application.Commands.Blog.Article
{
    public class GetArticleCommand : IRequest<ArticleDTO>
	{
		public Guid Id { get; set; }		
	}
	public class GetArticleCommandHandler(IArticleRepository articleRepository) : IRequestHandler<GetArticleCommand, ArticleDTO>
	{
		private readonly IArticleRepository _articleRepository = articleRepository;

		public async Task<ArticleDTO> Handle(GetArticleCommand request, CancellationToken cancellationToken)
		{
			var model = await _articleRepository.GetArticleAsync(request.Id);
			return model;
		}
	}
}
