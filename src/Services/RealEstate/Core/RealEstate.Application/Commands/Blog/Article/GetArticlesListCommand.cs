using RealEstate.Application.DTOs.Blog;
using RealEstate.Application.DTOs.Blog.Queries;
using RealEstate.Application.Interfaces.IRepository.Blog;
using MediatR;

namespace RealEstate.Application.Commands.Blog.Article
{
    public class GetArticlesListCommand : IRequest<ArticleListDTO>
    {
        public required ArticleQueryDTO QueryModel { get; set; }
    }
    public class GetArticlesListCommandHandler(IArticleRepository articleRepository) : IRequestHandler<GetArticlesListCommand, ArticleListDTO>
    {
        private readonly IArticleRepository _articleRepository = articleRepository;

        public async Task<ArticleListDTO> Handle(GetArticlesListCommand request, CancellationToken cancellationToken)
        {
            var model = await _articleRepository.GetArticlesListAsync(request.QueryModel);
            return model;
        }
    }
}
