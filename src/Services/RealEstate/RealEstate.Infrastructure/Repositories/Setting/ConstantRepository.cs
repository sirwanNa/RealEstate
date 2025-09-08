using RealEstate.Application.DTOs.Setting;
using RealEstate.Application.DTOs.Setting.Queries;
using RealEstate.Application.Exceptions;
using RealEstate.Application.Interfaces.IData;
using RealEstate.Application.Interfaces.IRepository.Setting;
using RealEstate.Domain.Entities.Setting;
using RealEstate.Infrastructure.Repositories.Common;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Shared.Enums;

namespace RealEstate.Infrastructure.Repositories.Setting
{
    public class ConstantRepository(IUnitOfWork unitOfWork, IMapper mapper) : BaseRepository(unitOfWork,mapper), IConstantRepository
    {       
        public async Task<ConstanDTO> GetConstantAsync(Guid id)
        {
            var obj = await GetAll<Constant>(entity=>entity.Id == id).Include(p=>p.ConstantTitles).FirstOrDefaultAsync() ?? throw new NotFoundException("Constant");
            var model = new ConstanDTO
            {
                Type= obj.Type,
                Id = obj.Id,
                Title = obj.ConstantTitles.Where(p=>p.Language == Language.English).FirstOrDefault()?.Title,
                Title_Ar = obj.ConstantTitles.Where(p => p.Language == Language.Arabic).FirstOrDefault()?.Title,
                Title_Fa = obj.ConstantTitles.Where(p => p.Language == Language.Persian).FirstOrDefault()?.Title,
                Description = obj.ConstantTitles.Where(p => p.Language == Language.English).FirstOrDefault()?.Description,
                Description_Ar = obj.ConstantTitles.Where(p => p.Language == Language.Arabic).FirstOrDefault()?.Description,
                Description_Fa = obj.ConstantTitles.Where(p => p.Language == Language.Persian).FirstOrDefault()?.Description,
            };
            return model;
        }

        public async Task<ConstantListDTO> GetConstantsListAsync(ConstantQueryDTO queryModel)
        {
            var query = GetAll<ConstantTitle>(entity=> entity.Constant.Type == queryModel.Type && entity.Language == queryModel.Language);
            if (!string.IsNullOrEmpty(queryModel.Title))
            {
               query = query.Where(p=>p.Title.ToLower().Trim().Contains(queryModel.Title.ToLower().Trim()));
            }
            if (!string.IsNullOrEmpty(queryModel.Description))
            {
                query = query.Where(p => !string.IsNullOrEmpty(p.Description) && p.Description.ToLower().Trim().Contains(queryModel.Description.ToLower().Trim()));
            }
			var count = await query.CountAsync();
            var queryList = await query
                             .OrderBy(p=>p.Title)
                             .Skip(queryModel.PageSize * (queryModel.PageNumber - 1))
                             .Take(queryModel.PageSize).ToListAsync();
			var model = new ConstantListDTO
			{
                PagesCount = count / queryModel.PageSize + (count % queryModel.PageSize > 0 ? 1 : 0),             
				ItemsList = queryList.Select(p => new ConstantListItemDTO
				{
					Id = p.ConstantId,
					Title = p.Title,
					Description = p.Description
				}).ToList()
			};

			return model;
        }


        public async Task<int> CreateConstantAsync(ConstanDTO model, CancellationToken cancellationToken)
        {
            var obj = new Constant {Type = model.Type, CreatedDate = DateTime.Now };
            await addConstantTitle(model.Title,model.Description,Language.English, obj);
            await addConstantTitle(model.Title_Fa, model.Description_Fa,Language.Persian, obj);
            await addConstantTitle(model.Title_Ar, model.Description_Ar,Language.Arabic, obj);
            var code = await _unitOfWork.SaveChangesAsync(cancellationToken);
            return code;
        }

        private async Task addConstantTitle(string? title,string? description,Language language, Constant obj)
        {
            if (!string.IsNullOrEmpty(title))
            {
                var constantTitle = new ConstantTitle
                {
                    Title = title,
                    Description = description,
                    CreatedDate = DateTime.Now,
                    Constant = obj,
                    Language = language
                };
                await AddAsync(constantTitle);
            }
        }

        public async Task<int> UpdateConstantAsync(ConstanDTO model,CancellationToken cancellationToken)
        {
            var obj = await GetAsync<Constant>(model.Id) ?? throw new NotFoundException("Constant");
            await updateConstantTitle(model.Title, model.Description,Language.English, obj.Id);
            await updateConstantTitle(model.Title_Fa, model.Description_Fa,Language.Persian, obj.Id);
            await updateConstantTitle(model.Title_Ar, model.Description_Ar, Language.Arabic, obj.Id);
            //_mapper.Map(model, obj);
            var code = await _unitOfWork.SaveChangesAsync(cancellationToken);
            return code;
        }
        private async Task updateConstantTitle(string? title, string? description,Language language, Guid ConstantId)
        {
            if (!string.IsNullOrEmpty(title))
            {
                var obj = await GetAll<ConstantTitle>(entity => entity.ConstantId == ConstantId && entity.Language == language).FirstOrDefaultAsync();
                if (obj != null)
                {
                    obj.Title = title;
                    obj.Description = description;
                    Edit(obj);
                }
                else
                {
                    obj = new ConstantTitle { Title = title, Description = description, Language = language, ConstantId = ConstantId };
                    await AddAsync(obj);
                }
            }
        }
        public async Task<int> DeleteConstantAsync(Guid id, CancellationToken cancellationToken)
        {
            var obj = await GetAsync<Constant>(id) ?? throw new NotFoundException("Constant");
            Delete(obj);
            var code = await _unitOfWork.SaveChangesAsync(cancellationToken);
            return code;
        }
    }
}
