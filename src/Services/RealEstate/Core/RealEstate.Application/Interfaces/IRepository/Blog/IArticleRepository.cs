using RealEstate.Application.DTOs.Blog;
using RealEstate.Application.DTOs.Blog.Queries;
using RealEstate.Application.DTOs.RealEstate;
using Shared.Enums;

namespace RealEstate.Application.Interfaces.IRepository.Blog
{
    public interface IArticleRepository
	{
		Task<ArticleDTO> GetArticleAsync(Guid id);
		Task<ArticleDTO> GetArticleAsync(string urlTitle, Language language);
		Task<ArticleDetailsDTO> GetArticleDetailAsync(string urlTitle, Language language);
        Task<ArticleListDTO> GetArticlesListAsync(ArticleQueryDTO queryModel);
		Task<List<ArticleListItemEndUserDTO>> GetArticlesListAsync(int totalOfItems, Language language);
        Task<int> CreateArticletAsync(ArticleDTO model, CancellationToken cancellationToken);
		Task<UploadFileResultDTO> UpdateArticleAsync(ArticleDTO model, CancellationToken cancellationToken);
		Task<int> DeleteArticleAsync(Guid id, CancellationToken cancellationToken);
	}
}
