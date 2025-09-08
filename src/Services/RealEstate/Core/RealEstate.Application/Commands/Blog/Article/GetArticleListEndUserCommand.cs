using RealEstate.Application.DTOs.Blog;
using RealEstate.Application.Interfaces.IRepository.Blog;
using MediatR;
using Shared.Enums;

namespace RealEstate.Application.Commands.Blog.Article
{
    public class GetArticleListEndUserCommand : IRequest<List<ArticleListItemEndUserDTO>>
    {
        public Language Language { get; set; }
        public int TotalOfItems { get; set; }
    }
    public class GetArticleListEndUserCommandHandler(IArticleRepository articleRepository) : IRequestHandler<GetArticleListEndUserCommand, List<ArticleListItemEndUserDTO>>
    {
        private readonly IArticleRepository _articleRepository = articleRepository;

        public async Task<List<ArticleListItemEndUserDTO>> Handle(GetArticleListEndUserCommand request, CancellationToken cancellationToken)
        {
            var model = await _articleRepository.GetArticlesListAsync(request.TotalOfItems,request.Language);
            return model;
        }
    }
}
