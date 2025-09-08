using RealEstate.Application.DTOs.Blog;
using RealEstate.Application.Interfaces.IRepository.Blog;
using MediatR;
using Shared.Enums;

namespace RealEstate.Application.Commands.Blog.Article
{
    public class GetArticleEndUserCommand : IRequest<ArticleDetailsDTO>
    {
        public required string UrlTitle { get; set; }
        public required Language Language { get; set; } 
    }
    public class GetArticleEndUserCommandHandler(IArticleRepository articleRepository) : IRequestHandler<GetArticleEndUserCommand, ArticleDetailsDTO>
    {
        private readonly IArticleRepository _articleRepository = articleRepository;

        public async Task<ArticleDetailsDTO> Handle(GetArticleEndUserCommand request, CancellationToken cancellationToken)
        {
            var model = await _articleRepository.GetArticleDetailAsync(request.UrlTitle,request.Language);
            return model;
        }
    }
}
