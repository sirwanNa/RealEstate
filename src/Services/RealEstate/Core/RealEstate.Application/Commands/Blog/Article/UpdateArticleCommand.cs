using RealEstate.Application.DTOs.Blog;
using RealEstate.Application.DTOs.RealEstate;
using RealEstate.Application.Interfaces.IRepository.Blog;
using MediatR;

namespace RealEstate.Application.Commands.Blog.Article
{
    public class UpdateArticleCommand : IRequest<UploadFileResultDTO>
    {
        public required ArticleDTO Model { get; set; }
    }
    public class UpdateArticleCommandHandler(IArticleRepository articleRepository) : IRequestHandler<UpdateArticleCommand, UploadFileResultDTO>
    {
        private readonly IArticleRepository _articleRepository = articleRepository;
        public async Task<UploadFileResultDTO> Handle(UpdateArticleCommand request, CancellationToken cancellationToken)
        {
            var result = await _articleRepository.UpdateArticleAsync(request.Model, cancellationToken);
            return result;
        }
    }
}
