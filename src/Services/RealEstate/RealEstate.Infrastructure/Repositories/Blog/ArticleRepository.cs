using System.Text.RegularExpressions;
using RealEstate.Application.DTOs.Blog;
using RealEstate.Application.DTOs.Blog.Queries;
using RealEstate.Application.DTOs.Common;
using RealEstate.Application.DTOs.RealEstate;
using RealEstate.Application.Exceptions;
using RealEstate.Application.Interfaces.IData;
using RealEstate.Application.Interfaces.IRepository.Blog;
using RealEstate.Domain.Entities.Blog;
using RealEstate.Domain.Entities.Setting;
using RealEstate.Infrastructure.Repositories.Common;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Shared.Enums;

namespace RealEstate.Infrastructure.Repositories.Blog
{
    public class ArticleRepository(IUnitOfWork unitOfWork, IMapper mapper) : BaseRepository(unitOfWork, mapper), IArticleRepository
	{
		public async Task<ArticleDTO> GetArticleAsync(Guid id)
		{
			var obj = await GetAll<Article>(entity => entity.Id == id).Include(p => p.ArticleTags).FirstOrDefaultAsync() ?? throw new NotFoundException("Article");
			var model = new ArticleDTO
			{			
				Id = obj.Id,
				Title = obj.Title,
				Description = obj.Description,	
				SelectedTagIdsList = obj.ArticleTags?.Select(p=>p.TagId).ToList(),
			};
            model.FilesList = await getDocumentsList(id);
            return model;
		}
        public async Task<ArticleDTO> GetArticleAsync(string urlTitle, Language language)
        {
            var obj = await GetAll<Article>(entity => entity.UrlTitle == urlTitle && entity.Language == language).Include(p => p.ArticleTags).FirstOrDefaultAsync();
            if(obj != null)
            {
                var model = new ArticleDTO
                {
                    Id = obj.Id,
                    Title = obj.Title,
                    Description = obj.Description,
                    SelectedTagIdsList = obj.ArticleTags?.Select(p => p.TagId).ToList(),
                };
                return model;
            }
            return new ArticleDTO();

        }
        public async Task<ArticleDetailsDTO> GetArticleDetailAsync(string urlTitle, Language language)
        {
            var obj = await GetAll<Article>(entity => entity.UrlTitle == urlTitle && entity.Language == language).FirstOrDefaultAsync();
            if (obj != null)
            {
                var tagsList = GetAll<ArticleTag>(p => p.ArticleId == obj.Id).Include(p => p.Tag).Select(p => p.Tag);
                var titlesList = GetAll<ConstantTitle>(p => p.Language == language);
                var selectedTagsList = tagsList.Join(titlesList, p => p.Id, p => p.ConstantId, (first, second) => second.Title).Distinct().ToList();
                var model = new ArticleDetailsDTO
                {
                    Id = obj.Id,
                    Title = obj.Title,
                    Description = obj.Description,
                    SelectedTagsList = selectedTagsList,
                };
                return model;
            }
            return new ArticleDetailsDTO();

        }
        public async Task<ArticleListDTO> GetArticlesListAsync(ArticleQueryDTO queryModel)
		{
			var query = GetAll<Article>(entity => entity.Language == queryModel.Language);
			if (!string.IsNullOrEmpty(queryModel.Title))
			{
				query = query.Where(p => p.Title.ToLower().Trim().Contains(queryModel.Title.ToLower().Trim()));
			}
            query = query.OrderByDescending(p=>p.CreatedDate);
			var count = await query.CountAsync();
			var queryList = await query.
							 Skip(queryModel.PageSize * (queryModel.PageNumber - 1))
							.Take(queryModel.PageSize).ToListAsync();
			var model = new ArticleListDTO
			{
				PagesCount = count / queryModel.PageSize + (count % queryModel.PageSize > 0 ? 1 : 0),
				ItemsList = queryList.Select(p => new ArticleListItemDTO
				{
					Id = p.Id,
					Title = p.Title,	
                    UrlTitle = p.UrlTitle
				}).ToList()
			};

			return model;
		}
        public async Task<List<ArticleListItemEndUserDTO>> GetArticlesListAsync(int totalOfItems,Language language)
        {
            var query = GetAll<Article>(entity => entity.Language == language);
            var documentsList = GetAll<Document>();

            var tempQuery = query.Join(documentsList, p => p.Id, p => p.RelatedId, (first, second) => new
            {
                first.Id,
                first.Title,
                first.UrlTitle,
                first.Description,
                first.CreatedDate,
                second.FilePath,
                second.FileName
            });

            var tempQueryList = await tempQuery.OrderByDescending(p => p.CreatedDate).Take(totalOfItems).ToListAsync();
          
            var model = tempQueryList.Select(p=>new ArticleListItemEndUserDTO
            {   
                Id = p.Id,
                Description = p.Description,
                Title = p.Title,
                UrlTitle = p.UrlTitle,
                MainImagePath = p.FilePath,
                FileName = p.FileName
            }).ToList();

            return model;
        }
        public async Task<int> CreateArticletAsync(ArticleDTO model, CancellationToken cancellationToken)
        {
            if(!string.IsNullOrEmpty(model.Title) && !string.IsNullOrEmpty(model.Description))
            {
                var obj = new Article
                {
                    Title = model.Title,
                    UrlTitle = Regex.Replace(model.Title.Trim(), @"\s+", " ").Replace(" ", "_"),
                    Description = model.Description,
                    CreatedDate = DateTime.Now,
                    Language = model.Language
                };
                Add(obj);
                AddDocuments(model.FilesList, obj.Id);
                await AddTags(model.SelectedTagIdsList, obj, model.Language);
                var code = await _unitOfWork.SaveChangesAsync(cancellationToken);
                return code;
            }
            return 0;
        }
        private void AddDocuments(List<FileUploadViewModelPost>? filesList, Guid id)
        {
            if (filesList != null)
            {
                foreach (var file in filesList)
                {
                    var document = new Document
                    {
                        FileName = file.FileName,
                        CreatedDate = DateTime.Now.Date,
                        EntityType = EntityType.Article,
                        FilePath = file.FilePath,
                        RelatedId = id
                    };
                    Add(document);
                }
            }
        }
        private async Task AddTags(List<Guid>? selectedTagIdsList, Article obj,Language language)
        {
            if (selectedTagIdsList != null)
            {
                foreach (var tagId in selectedTagIdsList)
                {
                    var articleTag = new ArticleTag
                    {
                        TagId = tagId,
                        Article = obj,
						Language = language,
						CreatedDate = DateTime.Now.Date
					};
                    await AddAsync(articleTag);
                }
            }
        }

		public async Task<UploadFileResultDTO> UpdateArticleAsync(ArticleDTO model, CancellationToken cancellationToken)
        {
            if (!string.IsNullOrEmpty(model.Title) && !string.IsNullOrEmpty(model.Description))
            {
                var obj = await GetAsync<Article>(model.Id) ?? throw new NotFoundException("Article");
                obj.Title = model.Title;
                obj.UrlTitle = Regex.Replace(model.Title.Trim(), @"\s+", " ").Replace(" ", "_");
                obj.Description = model.Description;
                Edit(obj);
                await UpdateArticleTags(model, obj);
                var result = await UpdateDocuments(model, obj);
                var code = await _unitOfWork.SaveChangesAsync(cancellationToken);
                return result;
            }
            return new UploadFileResultDTO
            {
                Code =0 
            };
        }
        private async Task<UploadFileResultDTO> UpdateDocuments(ArticleDTO model, Article obj)
        {
            var result = new UploadFileResultDTO();
            var oldDocumentsList = await getDocumentsList(model.Id);
            if (oldDocumentsList != null && model.FilesList != null)
            {
                var sharedList = oldDocumentsList.Join(model.FilesList, p => p.FileName, p => p.FileName, (first, second) => first).ToList();
                var deletedFilesList = oldDocumentsList.Except(sharedList).ToList();
                await deleteFiles(deletedFilesList);

                var sharedList_Files = oldDocumentsList.Join(model.FilesList, p => p.FileName, p => p.FileName, (first, second) => second).ToList();
                var addedFilesList = model.FilesList.Except(sharedList_Files).ToList();
                AddDocuments(addedFilesList, obj.Id);
                result.DeletedFilesList = deletedFilesList;
                result.AddedFilesList = addedFilesList;
            }
            else if (oldDocumentsList == null && model.FilesList != null)
            {
                AddDocuments(model.FilesList, obj.Id);
                result.AddedFilesList = model.FilesList;
            }
            else if (model.FilesList == null && oldDocumentsList != null)
            {
                await deleteFiles(oldDocumentsList);
                result.DeletedFilesList = oldDocumentsList;
            }
            return result;
        }
        private async Task deleteFiles(List<FileUploadViewModelPost>? deletedFilesList)
        {
            if (deletedFilesList != null)
            {
                foreach (var item in deletedFilesList)
                {
                    var document = await GetAsync<Document>(item.Id);
                    if (document != null)
                    {
                        Delete(document);
                    }
                }
            }
        }
        private async Task UpdateArticleTags(ArticleDTO model, Article obj)
        {
            var oldSelectedTagIdsList = await GetAll<ArticleTag>(entity => entity.ArticleId == model.Id && entity.Language == model.Language).Select(p => p.TagId).ToListAsync();
            await AddArticleTags(model, obj, oldSelectedTagIdsList);
            await DeleteArticleTags(model, obj, oldSelectedTagIdsList);
        }

        private async Task AddArticleTags(ArticleDTO model, Article obj, List<Guid> oldSelectedTagIdsList)
        {
            if (model.SelectedTagIdsList != null)
            {
                var shared_Add = model.SelectedTagIdsList.Join(oldSelectedTagIdsList, p => p, p => p, (first, second) => first).ToList();
                var addedTagsList = model.SelectedTagIdsList.Except(shared_Add).ToList();
                await AddTags(addedTagsList, obj, model.Language);
            }
        }

        private async Task DeleteArticleTags(ArticleDTO model, Article obj, List<Guid> oldSelectedTagIdsList)
        {
            if (model.SelectedTagIdsList != null)
			{
                var shared_Delete = oldSelectedTagIdsList.Join(model.SelectedTagIdsList, p => p, p => p, (first, second) => first).ToList();
                var deletedTagsList = oldSelectedTagIdsList.Except(shared_Delete).ToList();
                if (deletedTagsList != null && deletedTagsList.Any())
                {
                    foreach (var tagId in deletedTagsList)
                    {
                        var articleTag = await GetAll<ArticleTag>(entity => entity.ArticleId == obj.Id && entity.TagId == tagId).FirstOrDefaultAsync();
                        if (articleTag != null)
                        {
                            Delete(articleTag);
                        }
                    }
                }
            }
        }

		public async Task<int> DeleteArticleAsync(Guid id, CancellationToken cancellationToken)
		{
			var obj = await GetAsync<Article>(id) ?? throw new NotFoundException("Article"); 
			Delete(obj);
			var code = await _unitOfWork.SaveChangesAsync(cancellationToken);
			return code;
		}
        private async Task<List<FileUploadViewModelPost>> getDocumentsList(Guid id)
        {
            return await GetAll<Document>(entity => entity.RelatedId == id && entity.EntityType == EntityType.Article)
                .Select(p => new FileUploadViewModelPost
                {
                    FileName = p.FileName,
                    FilePath = p.FilePath,
                    Id = p.Id

                }).ToListAsync();
        }
    }
}
