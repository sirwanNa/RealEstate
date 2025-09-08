using System.Text.RegularExpressions;
using RealEstate.Application.DTOs.Common;
using RealEstate.Application.DTOs.RealEstate;
using RealEstate.Application.DTOs.RealEstate.Queries;
using RealEstate.Application.Exceptions;
using RealEstate.Application.Interfaces.IData;
using RealEstate.Application.Interfaces.IRepository.RealEstate;
using RealEstate.Domain.Entities.RealEstate;
using RealEstate.Domain.Entities.Setting;
using RealEstate.Infrastructure.Repositories.Common;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Shared.Enums;

namespace RealEstate.Infrastructure.Repositories.RealEstate
{
    public class PropertyInventoryRepository(IUnitOfWork unitOfWork, IMapper mapper) : BaseRepository(unitOfWork, mapper), IPropertyInventoryRepository
	{
		public async Task<PropertyInventoryDTO> GetPropertyInventoryAsync(Guid id)
		{
			var obj = await GetAll<PropertyInventory>(entity => entity.Id == id)
                .Include(p => p.PropertyInventoryTitles)
                .Include(p => p.PropertyTags)
				.Include(p=>p.PropertyTypes)
				.Include(p=>p.PropertyFeatures)
                .FirstOrDefaultAsync() ?? throw new NotFoundException("PropertyInventory");
			var model = new PropertyInventoryDTO
			{
				Id = obj.Id,
				BuilderId = obj.BuilderId,
				RegionId = obj.RegionId,
				Capacity = obj.Capacity,
				CreatedDate = obj.CreatedDate,
				Currency = obj.Currency,
				FinishDate = obj.FinishDate,
				IsDeleted = obj.IsDeleted,
				Price = obj.Price,
				PositionType = obj.PositionType,
				//RealEstateType = obj.RealEstateType,
				StartDate = obj.StartDate,
				StructureType = obj.StructureType,
				TotalOfRooms = obj.TotalOfRooms,		
				Prepayment = obj.Prepayment,
				DuringProjectPayment = obj.DuringProjectPayment,
				HandoverPayment = obj.HandoverPayment,
				AfterHandoverPayment = obj.AfterHandoverPayment,
				SelectedTypesList = obj.PropertyTypes?.Select(p => p.RealEstateType).ToList(),
				SelectedFeaturesList = obj.PropertyFeatures?.Select(p => p.FeatureType).ToList(),
				BrochureLink = obj.BrochureLink,
                Title = obj.PropertyInventoryTitles?.Where(p => p.Language == Language.English).FirstOrDefault()?.Title,
				Title_Ar = obj.PropertyInventoryTitles?.Where(p => p.Language == Language.Arabic).FirstOrDefault()?.Title,
				Title_Fa = obj.PropertyInventoryTitles?.Where(p => p.Language == Language.Persian).FirstOrDefault()?.Title,
				Description = obj.PropertyInventoryTitles?.Where(p => p.Language == Language.English).FirstOrDefault()?.Description,
				Description_Ar = obj.PropertyInventoryTitles?.Where(p => p.Language == Language.Arabic).FirstOrDefault()?.Description,
				Description_Fa = obj.PropertyInventoryTitles?.Where(p => p.Language == Language.Persian).FirstOrDefault()?.Description,
                ShortDescription = obj.PropertyInventoryTitles?.Where(p => p.Language == Language.English).FirstOrDefault()?.ShortDescription,
                ShortDescription_Ar = obj.PropertyInventoryTitles?.Where(p => p.Language == Language.Arabic).FirstOrDefault()?.ShortDescription,
                ShortDescription_Fa = obj.PropertyInventoryTitles?.Where(p => p.Language == Language.Persian).FirstOrDefault()?.ShortDescription,
                SelectedTagIdsList_En = obj.PropertyTags?.Where(p=> p.Language == Language.English).Select(p =>p.TagId).ToList(),
				SelectedTagIdsList_Ar = obj.PropertyTags?.Where(p => p.Language == Language.Arabic).Select(p => p.TagId).ToList(),
				SelectedTagIdsList_Fa = obj.PropertyTags?.Where(p => p.Language == Language.Persian).Select(p => p.TagId).ToList(),
			};
            model.FilesList = await getDocumentsList(id);
            return model;
		}

        public async Task<PropertyInventoryEndUserDTO> GetPropertyInventoryAsync(string urlTitle, Language language, int totalOfRandomItems)
        {
            var obj = await GetAll<PropertyInventoryTitle>(entity => entity.UrlTitle == urlTitle && entity.Language == language)
                .Include(p=>p.PropertyInventory)
                .FirstOrDefaultAsync();
            if(obj != null && obj.PropertyInventory != null)
			{
				var selectedTagsList = await GetSelectedTagsList(language, obj.PropertyInventoryId);

				var region = await GetConstant(language, obj.PropertyInventory.RegionId, ConstantType.Region);

                var builder = await GetConstant(language, obj.PropertyInventory.BuilderId, ConstantType.Builder);

				var propertyFeaturesList = await GetAll<PropertyFeature>(p => p.PropertyInventoryId == obj.PropertyInventoryId).ToListAsync();
				var amenitiesList = propertyFeaturesList != null && propertyFeaturesList.Any() ? string.Join(',', propertyFeaturesList.Select(p => p.FeatureType.GetDisplayName())) : string.Empty;

				var model = new PropertyInventoryEndUserDTO
				{
					Capacity = obj.PropertyInventory.Capacity,
					Currency = obj.PropertyInventory.Currency,
					FinishDate = obj.PropertyInventory.FinishDate,
					Price = obj.PropertyInventory.Price,
					Prepayment = obj.PropertyInventory.Prepayment,
					DuringProjectPayment = obj.PropertyInventory.DuringProjectPayment,
					HandoverPayment = obj.PropertyInventory.HandoverPayment,
					AfterHandoverPayment = obj.PropertyInventory.AfterHandoverPayment,
					Builder = builder?.Title,
					AmenitiesList = amenitiesList,
					//RealEstateType = obj.PropertyInventory.RealEstateType,
					StartDate = obj.PropertyInventory.StartDate,
					StructureType = obj.PropertyInventory.StructureType,
					TotalOfRooms = obj.PropertyInventory.TotalOfRooms,
					Title = obj.PropertyInventory.PropertyInventoryTitles?.Where(p => p.Language == language).FirstOrDefault()?.Title,
					Description = obj.PropertyInventory.PropertyInventoryTitles?.Where(p => p.Language == language).FirstOrDefault()?.Description,
					ShortDescription = obj.PropertyInventory.PropertyInventoryTitles?.Where(p => p.Language == language).FirstOrDefault()?.ShortDescription,
					Region = region?.Title,
					SelectedTagsList = selectedTagsList,
					BrochureLink = obj.PropertyInventory.BrochureLink
				};
				model.FilesList = await getDocumentsList(obj.PropertyInventoryId);

				await SetRecommendationsList(language, totalOfRandomItems, obj, model);
				return model;
			}
			return new PropertyInventoryEndUserDTO();

        }

		private async Task<ConstantTitle?> GetConstant(Language language, Guid regionId, ConstantType type)
		{
			return await GetAll<ConstantTitle>(entity => entity.Constant.Type == type
                             && entity.Language == language
							 && entity.ConstantId == regionId)
							 .FirstOrDefaultAsync();
		}

		private async Task<List<string>> GetSelectedTagsList(Language language, Guid propertyInventoryId)
		{
			var propertyTagsList = GetAll<PropertyTag>(entity => entity.PropertyInventoryId == propertyInventoryId)
									.Include(p => p.Tag).Select(p => p.Tag);
			var constantTitlesList = GetAll<ConstantTitle>(entity => entity.Constant.Type == ConstantType.Tag && entity.Language == language);

			var selectedTagsList = await propertyTagsList.Join(constantTitlesList, p => p.Id, p => p.ConstantId, (first, second) => second.Title).ToListAsync();
			return selectedTagsList;
		}

		private async Task SetRecommendationsList(Language language, int totalOfRandomItems, PropertyInventoryTitle? obj, PropertyInventoryEndUserDTO model)
        {
			if(obj != null && obj.PropertyInventory != null)
			{
                var randomProperyInventoriesList = GetAll<PropertyInventory>(entity => entity.Id != obj.PropertyInventoryId /*&& entity.RegionId == obj.PropertyInventory.RegionId*/);
                var randomPropertyInventoryTitles = GetAll<PropertyInventoryTitle>(entity => entity.Language == language);
				var tempQuery = randomProperyInventoriesList.Join(randomPropertyInventoryTitles, p => p.Id, p => p.PropertyInventoryId, (first, second) => new
				{
					first.Price,
					first.Currency,
					second.Title,
					second.UrlTitle,
					second.PropertyInventoryId
				});
				var documentsList = GetAll<Document>(entity => entity.EntityType == EntityType.PropertyInventor);
                model.RecommendationList= await tempQuery.Join(documentsList, p => p.PropertyInventoryId, p => p.RelatedId, (first, second) => new PropertyInventoryRecommendationDTO
                {
                    PropertyInventoryId = first.PropertyInventoryId,
                    Title = first.Title,
                    UrlTitle = first.UrlTitle,
                    FileName = second.FileName,
					Price = first.Price,
					Currency = first.Currency

                }).GroupBy(p => p.PropertyInventoryId).Select(p => p.FirstOrDefault()).OrderBy(_ => Guid.NewGuid()).Take(totalOfRandomItems).ToListAsync();
            }
        }

        public async Task<PropertyInventoryListDTO> GetPropertyInventoriesListAsync(PropertyInventoryQueryDTO queryModel)
		{
			var query = GetAll<PropertyInventory>();
			query = ApplyFilter(queryModel, query);
			var query_Titles = GetAll<PropertyInventoryTitle>(entity => entity.Language == queryModel.Language);
			if (!string.IsNullOrEmpty(queryModel.Title))
			{
				query_Titles = query_Titles.Where(p => !string.IsNullOrEmpty(p.Title) && p.Title.Trim().Contains(queryModel.Title));               
			}
            if (!string.IsNullOrEmpty(queryModel.Description))
            {
                query_Titles = query_Titles.Where(p => !string.IsNullOrEmpty(p.Description) && p.Title.Trim().Contains(queryModel.Description));
            }
			var tempQuery = query.Join(query_Titles, p => p.Id, p => p.PropertyInventoryId, (first, second) => new
			{
				first.Id,
				first.BuilderId,
				first.RegionId,
				first.Capacity,
				first.CreatedDate,
				first.Currency,
				first.FinishDate,
				first.IsDeleted,
				first.Price,
				//first.RealEstateType,
				first.StartDate,
				first.StructureType,
				first.TotalOfRooms,
				second.Title,
				second.UrlTitle,
				second.Description
			});
			var count = await tempQuery.CountAsync();
			if (queryModel.OrderBy == PropertyInventoryOrderType.Default)
			{
				tempQuery = tempQuery.OrderBy(p => p.Title);
			}
			else if (queryModel.OrderBy == PropertyInventoryOrderType.Price_Low_To_High)
			{
				tempQuery = tempQuery.OrderBy(p => p.Price);
			}
			else if (queryModel.OrderBy == PropertyInventoryOrderType.Price_High_To_Low)
			{
				tempQuery = tempQuery.OrderByDescending(p => p.Price);
			}
			else if (queryModel.OrderBy == PropertyInventoryOrderType.Date_Old_To_New)
			{
				tempQuery = tempQuery.OrderBy(p => p.StartDate);
			}
			else if (queryModel.OrderBy == PropertyInventoryOrderType.Date_New_To_Old)
			{
				tempQuery = tempQuery.OrderByDescending(p => p.StartDate);
			}
            else if (queryModel.OrderBy == PropertyInventoryOrderType.CreateDate)
            {
                tempQuery = tempQuery.OrderByDescending(p => p.CreatedDate);
            }
            tempQuery = tempQuery
                 .Skip(queryModel.PageSize * (queryModel.PageNumber - 1))
                 .Take(queryModel.PageSize);

            //var queryList = tempQuery;							 

            var constants = GetAll<ConstantTitle>(entity => entity.Language == queryModel.Language);
			var regionsList = constants.Where(p => p.Constant.Type == ConstantType.Region);
			var buildresList = constants.Where(p => p.Constant.Type == ConstantType.Builder);
			var tempQuery_Regions = regionsList.Join(tempQuery, p => p.ConstantId, p => p.RegionId, (first, second) => new
			{
				second.Id,
				second.BuilderId,
				second.RegionId,
				second.Capacity,
				second.CreatedDate,
				second.Currency,
				second.FinishDate,
				second.IsDeleted,
				second.Price,
				//second.RealEstateType,
				second.StartDate,
				second.StructureType,
				second.TotalOfRooms,
				second.Title,
				second.UrlTitle,
				second.Description,
				Region = first.Title
			});

			var documentsList = GetAll<Document>(entity => entity.EntityType == EntityType.PropertyInventor);
			var tempQuery_Documents = await tempQuery.Join(documentsList, p => p.Id, p => p.RelatedId, (first, second) => new
			{
				first.Id,
				second.FileName,
				second.FilePath
			}).ToListAsync();

			tempQuery_Documents = tempQuery_Documents?.GroupBy(p => p.Id).Select(p => p.FirstOrDefault()).ToList();

			var tempQuery_Builders = await buildresList.Join(tempQuery_Regions, p => p.ConstantId, p => p.BuilderId, (first, second) => new
			{
				second.Id,
				second.BuilderId,
				second.RegionId,
				second.Capacity,
				second.CreatedDate,
				second.Currency,
				second.FinishDate,
				second.IsDeleted,
				second.Price,
				//second.RealEstateType,
				second.StartDate,
				second.StructureType,
				second.TotalOfRooms,
				second.Title,
				second.UrlTitle,
				second.Description,
				second.Region,
				//second.FileName,
				//second.FilePath,
				Builder = first.Title
			}).ToListAsync();

            var propertyTypes = GetAll<PropertyType>();
            var propertyTypesList = await tempQuery.Join(propertyTypes, p => p.Id, p => p.PropertyInventoryId, (first, second) => second).ToListAsync();

            var model = new PropertyInventoryListDTO
			{
				PagesCount = count / queryModel.PageSize + (count % queryModel.PageSize > 0 ? 1 : 0),
				ItemsList = tempQuery_Builders.Select(p =>
				{
					var document = tempQuery_Documents?.Where(document => document.Id == p.Id).FirstOrDefault();
					var result = new PropertyInventoryListItemDTO
					{
						Id = p.Id,
						Capacity = p.Capacity,
						CreatedDate = p.CreatedDate,
						Currency = p.Currency,
						FinishDate = p.FinishDate,
						Price = p.Price,
						RealEstateTypesList =string.Join(',', propertyTypesList?.Where(type=> type.PropertyInventoryId == p.Id).Select(p=>p.RealEstateType.ToString())),
						StartDate = p.StartDate,
						StructureType = p.StructureType,
						TotalOfRooms = p.TotalOfRooms,
						Title = p.Title,
						UrlTitle = p.UrlTitle,
						Description =p.Description,
						Region = p.Region,
						Builder = p.Builder,
						MainFileName = document?.FileName,
						MainFilePath = document?.FilePath
					};
					return result;
				}
				).ToList()
			};
			SortItems(queryModel, model);
			return model;
		}

		private  void SortItems(PropertyInventoryQueryDTO queryModel, PropertyInventoryListDTO model)
		{
			if (model.ItemsList != null)
			{
				if (queryModel.OrderBy == PropertyInventoryOrderType.Default)
				{
					model.ItemsList = model.ItemsList.OrderBy(p => p.Title).ToList();
				}
				else if (queryModel.OrderBy == PropertyInventoryOrderType.Price_Low_To_High)
				{
					model.ItemsList = model.ItemsList.OrderBy(p => p.Price).ToList();
				}
				else if (queryModel.OrderBy == PropertyInventoryOrderType.Price_High_To_Low)
				{
					model.ItemsList = model.ItemsList.OrderByDescending(p => p.Price).ToList();
				}
				else if (queryModel.OrderBy == PropertyInventoryOrderType.Date_Old_To_New)
				{
					model.ItemsList = model.ItemsList.OrderBy(p => p.StartDate).ToList();
				}
				else if (queryModel.OrderBy == PropertyInventoryOrderType.Date_New_To_Old)
				{
					model.ItemsList = model.ItemsList.OrderByDescending(p => p.StartDate).ToList();
				}
                else if (queryModel.OrderBy == PropertyInventoryOrderType.CreateDate)
                {
                    model.ItemsList = model.ItemsList.OrderByDescending(p => p.CreatedDate).ToList();
                }
            }
		}

		private  IQueryable<PropertyInventory> ApplyFilter(PropertyInventoryQueryDTO queryModel, IQueryable<PropertyInventory> query)
        {
            if (queryModel.StructureType.HasValue)
            {
                query = query.Where(p => p.StructureType == queryModel.StructureType.Value);
            }
			if (queryModel.RealEstateType.HasValue)
			{
                var projectTypes = GetAll<PropertyType>(p=>p.RealEstateType == queryModel.RealEstateType.Value);
                query = query.Join(projectTypes, p=>p.Id,p=>p.PropertyInventoryId,(first,second)=>first);
			}
			if (!string.IsNullOrEmpty(queryModel.StartDate))
            {
                query = query.Where(p => !string.IsNullOrEmpty(p.StartDate) && p.StartDate.Trim().Contains(queryModel.StartDate, StringComparison.CurrentCultureIgnoreCase));
            }
            if (!string.IsNullOrEmpty(queryModel.FinishDate))
            {
                query = query.Where(p => !string.IsNullOrEmpty(p.FinishDate) && p.FinishDate.Trim().Contains(queryModel.FinishDate, StringComparison.CurrentCultureIgnoreCase));
            }
            if (queryModel.Price_From.HasValue)
            {
                query = query.Where(p => p.Price >= queryModel.Price_From.Value);
            }
            if (queryModel.Price_To.HasValue)
            {
                query = query.Where(p => p.Price <= queryModel.Price_To.Value);
            }
            //if (queryModel.Capacity_From.HasValue)
            //{
            //    query = query.Where(p => p.Price >= queryModel.Capacity_From.Value);
            //}
            //if (queryModel.Capacity_To.HasValue)
            //{
            //    query = query.Where(p => p.Price <= queryModel.Capacity_To.Value);
            //}
            if (queryModel.RegionId.HasValue)
            {
                query = query.Where(p => p.RegionId == queryModel.RegionId.Value);
            }
            if (queryModel.BuilderId.HasValue)
            {
                query = query.Where(p => p.BuilderId == queryModel.BuilderId.Value);
            }
            //if (queryModel.TotalOfRooms.HasValue)
            //{
            //    query = query.Where(p => p.TotalOfRooms == queryModel.TotalOfRooms.Value);
            //}
            return query;
        }

        public async Task<int> CreatePropertyInventoryAsync(PropertyInventoryDTO model, CancellationToken cancellationToken)
        {
            //var obj = _mapper.Map<PropertyInventory>(model);
            var obj = new PropertyInventory
            {
                BuilderId = model.BuilderId,
                RegionId = model.RegionId,
                Currency = model.Currency,
                Price = model.Price,
                Capacity = model.Capacity,
                CreatedDate = DateTime.Now,
                FinishDate = model.FinishDate,
                //RealEstateType = model.RealEstateType,
                StartDate = model.StartDate,
                StructureType = model.StructureType,
                TotalOfRooms = model.TotalOfRooms,
                Prepayment = model.Prepayment,
                DuringProjectPayment = model.DuringProjectPayment,
                HandoverPayment = model.HandoverPayment,
				BrochureLink = model.BrochureLink,
				PositionType =model.PositionType,
				AfterHandoverPayment = model.AfterHandoverPayment,
                Id = Guid.NewGuid()

            };
            await AddPropertyInventoryTitle(model.Title, model.Description, model.ShortDescription, Language.English, obj);
            await AddPropertyInventoryTitle(model.Title_Fa, model.Description_Fa, model.ShortDescription_Fa, Language.Persian, obj);
            await AddPropertyInventoryTitle(model.Title_Ar, model.Description_Ar, model.ShortDescription_Ar, Language.Arabic, obj);
            AddDocuments(model.FilesList, obj.Id);
            await AddTags(model.SelectedTagIdsList_Fa, obj, Language.Persian);
            await AddTags(model.SelectedTagIdsList_Ar, obj, Language.Arabic);
            await AddTags(model.SelectedTagIdsList_En, obj, Language.English);
            await AddPropertyTypes(model.SelectedTypesList, obj);
            await AddPropertyFeatures(model.SelectedFeaturesList, obj);
            var code = await _unitOfWork.SaveChangesAsync(cancellationToken);
            return code;
        }
        public async Task<int> CreatePropertyInventoryAsync(PropertyInventoryFromFilesDTO model, CancellationToken cancellationToken)
		{
			if (!string.IsNullOrEmpty(model.Region))
            {
                var postModel = _mapper.Map<PropertyInventoryDTO>(model);
                await AddRegion(model, postModel);
                await AddBuilder(model, postModel);
                return await CreatePropertyInventoryAsync(postModel, cancellationToken);
            }
            return 0;
        }

        private async Task AddRegion(PropertyInventoryFromFilesDTO model, PropertyInventoryDTO postModel)
        {
            if (!string.IsNullOrEmpty(model.Region))
            {
                var region = await GetAll<ConstantTitle>(p => p.Constant.Type == ConstantType.Region && p.Title == model.Region).FirstOrDefaultAsync();
                if (region != null)
                {
                    postModel.RegionId = region.ConstantId;
                }
                else
                {
                    var obj = new Constant
                    {
                        Type = ConstantType.Region,
                        CreatedDate = DateTime.Now,
                        Id = Guid.NewGuid()
                    };
                    var constantTitle_Fa = new ConstantTitle
                    {
                        Title = model.Region,
                        Language = Language.Persian,
                        Constant = obj
                    };
                    Add(constantTitle_Fa);
                    var constantTitle_Ar = new ConstantTitle
                    {
                        Title = model.Region,
                        Language = Language.Arabic,
                        Constant = obj
                    };
                    Add(constantTitle_Ar);
                    var constantTitle_En = new ConstantTitle
                    {
                        Title = model.Region,
                        Language = Language.English,
                        Constant = obj
                    };
                    Add(constantTitle_En);
                    postModel.RegionId = obj.Id;
                }
            }
        }
        private async Task AddBuilder(PropertyInventoryFromFilesDTO model, PropertyInventoryDTO postModel)
        {
            if (!string.IsNullOrEmpty(model.Builder))
            {
                var builder = await GetAll<ConstantTitle>(p => p.Constant.Type == ConstantType.Builder && p.Title == model.Builder).FirstOrDefaultAsync();
                if (builder != null)
                {
                    postModel.BuilderId = builder.ConstantId;
                }
                else
                {
                    var obj = new Constant
                    {
                        Type = ConstantType.Builder,
                        CreatedDate = DateTime.Now,
                        Id = Guid.NewGuid()
                    };
                    var constantTitle_Fa = new ConstantTitle
                    {
                        Title = model.Builder,
                        Language = Language.Persian,
                        Constant = obj
                    };
                    Add(constantTitle_Fa);
                    var constantTitle_Ar = new ConstantTitle
                    {
                        Title = model.Builder,
                        Language = Language.Arabic,
                        Constant = obj
                    };
                    Add(constantTitle_Ar);
                    var constantTitle_En = new ConstantTitle
                    {
                        Title = model.Builder,
                        Language = Language.English,
                        Constant = obj
                    };
                    Add(constantTitle_En);
                    postModel.BuilderId = obj.Id;
                }
            }
        }
        private async Task AddPropertyTypes(List<RealEstateType>? itemsList, PropertyInventory obj)
        {
            if (itemsList != null && itemsList.Any())
            {
                foreach (var type in itemsList)
                {
                    var propertyType = new PropertyType
                    {
                        CreatedDate = DateTime.Now,
                        PropertyInventory = obj,
                        RealEstateType = type,
                    };
                    await AddAsync(propertyType);
                }
            }
        }
        private async Task AddPropertyFeatures(List<FeatureType>? itemsList, PropertyInventory obj)
        {
            if (itemsList != null && itemsList.Any())
            {
                foreach (var type in itemsList)
                {
                    var propertyFeature = new PropertyFeature
                    {
                        CreatedDate = DateTime.Now,
                        PropertyInventory = obj,
                        FeatureType = type
                    };
                    await AddAsync(propertyFeature);
                }
            }
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
                        EntityType = EntityType.PropertyInventor,
                        FilePath = file.FilePath,
                        RelatedId = id
                    };
                    Add(document);
                }
            }
        }

        private async Task AddPropertyInventoryTitle(string? title, string? description,string? shortDescription, Language language, PropertyInventory obj)
		{
			if (!string.IsNullOrEmpty(title))
			{
				var propertyInventoryTitle = new PropertyInventoryTitle
				{
					Title = title,
                    UrlTitle = Regex.Replace(title.Trim(), @"\s+", " ").Replace(" ","_"), 
                    Description = description,
					ShortDescription = shortDescription,
					CreatedDate = DateTime.Now,
					PropertyInventory = obj,
					Language = language
				};
				await AddAsync(propertyInventoryTitle);
			}
		}
		private async Task AddTags(List<Guid>? selectedTagIdsList, PropertyInventory obj,Language language)
		{
			if (selectedTagIdsList != null)
			{
				foreach (var tagId in selectedTagIdsList)
				{
					var propertyTag = new PropertyTag
					{
						TagId = tagId,
						PropertyInventoryId = obj.Id,
                        Language = language,
						CreatedDate = DateTime.Now.Date
					};
					await AddAsync(propertyTag);
				}
			}
		}
		public async Task<UploadFileResultDTO> UpdatePropertyInventoryAsync(PropertyInventoryDTO model, CancellationToken cancellationToken)
        {
            var obj = await GetAll<PropertyInventory>(entity => entity.Id == model.Id).Include(p => p.PropertyInventoryTitles).FirstOrDefaultAsync() ?? throw new NotFoundException("Property Inventory");
            obj.BuilderId = model.BuilderId;
            obj.RegionId = model.RegionId;
            obj.Currency = model.Currency;
            obj.Price = model.Price;
            obj.Capacity = model.Capacity;
            obj.CreatedDate = DateTime.Now.Date;
            obj.FinishDate = model.FinishDate;
            //obj.RealEstateType = model.RealEstateType;
            obj.StartDate = model.StartDate;
            obj.StructureType = model.StructureType;
            obj.TotalOfRooms = model.TotalOfRooms;
            obj.Prepayment = model.Prepayment;
            obj.DuringProjectPayment = model.DuringProjectPayment;
            obj.HandoverPayment = model.HandoverPayment;
            obj.BrochureLink = model.BrochureLink;
			obj.PositionType = model.PositionType;
			obj.AfterHandoverPayment = model.AfterHandoverPayment;
            Edit(obj);

            var result = await UpdateDocuments(model, obj);
            await updatePropertyInventory(model.Title, model.Description, model.ShortDescription, Language.English, obj.Id);
            await updatePropertyInventory(model.Title_Fa, model.Description_Fa, model.ShortDescription_Fa, Language.Persian, obj.Id);
            await updatePropertyInventory(model.Title_Ar, model.Description_Ar, model.ShortDescription_Ar, Language.Arabic, obj.Id);
            await UpdatePropertyTags(model, obj);
            await UpdatePropertyTypesList(model, obj);
            await UpdatePropertyFeaturesList(model, obj);
            result.Code = await _unitOfWork.SaveChangesAsync(cancellationToken);
            return result;
        }

        private async Task UpdatePropertyTypesList(PropertyInventoryDTO model, PropertyInventory obj)
        {
            var oldPropertyTypesList = GetAll<PropertyType>(p => p.PropertyInventoryId == model.Id).ToList();
            if (model.SelectedTypesList != null)
            {
                var sharedList = model.SelectedTypesList.Join(oldPropertyTypesList, p => p, p => p.RealEstateType, (first, second) => second).ToList();
                var mustBeDeletedList = oldPropertyTypesList.Except(sharedList).ToList();
                DeletePropertyTypesList(mustBeDeletedList);

                var sharedList_Model = model.SelectedTypesList.Join(oldPropertyTypesList, p => p, p => p.RealEstateType, (first, second) => first).ToList();
                var mustBeAddedList = model.SelectedTypesList.Except(sharedList_Model).ToList();
                await AddPropertyTypes(mustBeAddedList, obj);
            }
            else
            {
                DeletePropertyTypesList(oldPropertyTypesList);
            }
        }
        private async Task UpdatePropertyFeaturesList(PropertyInventoryDTO model, PropertyInventory obj)
        {
            var oldPropertyFeaturesList = GetAll<PropertyFeature>(p => p.PropertyInventoryId == model.Id).ToList();
            if (model.SelectedFeaturesList != null)
            {
                var sharedList = model.SelectedFeaturesList.Join(oldPropertyFeaturesList, p => p, p => p.FeatureType, (first, second) => second).ToList();
                var mustBeDeletedList = oldPropertyFeaturesList.Except(sharedList).ToList();
                DeletePropertyFeaturesList(mustBeDeletedList);

                var sharedList_Model = model.SelectedFeaturesList.Join(oldPropertyFeaturesList, p => p, p => p.FeatureType, (first, second) => first).ToList();
                var mustBeAddedList = model.SelectedFeaturesList.Except(sharedList_Model).ToList();
                await AddPropertyFeatures(mustBeAddedList, obj);
            }
            else
            {
                DeletePropertyFeaturesList(oldPropertyFeaturesList);
            }
        }
        private void DeletePropertyTypesList(List<PropertyType>? itemsList)
        {
			if(itemsList != null && itemsList.Any())
			{
                foreach (var item in itemsList)
                {
                    Delete(item);
                }
            }
        }
        private void DeletePropertyFeaturesList(List<PropertyFeature>? itemsList)
        {
            if (itemsList != null && itemsList.Any())
            {
                foreach (var item in itemsList)
                {
                    Delete(item);
                }
            }
        }
        private async Task UpdatePropertyTags(PropertyInventoryDTO model, PropertyInventory obj)
		{
			var oldSelectedTagList = await GetAll<PropertyTag>(entity => entity.PropertyInventoryId == model.Id).ToListAsync();
            if (oldSelectedTagList != null)
            {
				var oldSelectedTagIdsList_En = oldSelectedTagList.Where(p => p.Language == Language.English).Select(p => p.TagId).ToList();
				await AddPropertyTags(model.SelectedTagIdsList_En, obj, oldSelectedTagIdsList_En,Language.English);
				await DeletePropertyInventoryTags(model.SelectedTagIdsList_En, obj, oldSelectedTagIdsList_En);

				var oldSelectedTagIdsList_Fa = oldSelectedTagList.Where(p => p.Language == Language.Persian).Select(p => p.TagId).ToList();
				await AddPropertyTags(model.SelectedTagIdsList_Fa, obj, oldSelectedTagIdsList_Fa, Language.Persian);
				await DeletePropertyInventoryTags(model.SelectedTagIdsList_Fa, obj, oldSelectedTagIdsList_Fa);

				var oldSelectedTagIdsList_Ar = oldSelectedTagList.Where(p => p.Language == Language.Arabic).Select(p => p.TagId).ToList();
				await AddPropertyTags(model.SelectedTagIdsList_Ar, obj, oldSelectedTagIdsList_Ar, Language.Arabic);
				await DeletePropertyInventoryTags(model.SelectedTagIdsList_Ar, obj, oldSelectedTagIdsList_Ar);
			}
		}

		private async Task AddPropertyTags(List<Guid>? selectedTagIdsList, PropertyInventory obj, List<Guid> oldSelectedTagIdsList,Language language)
		{
			if (selectedTagIdsList != null)
			{
				var shared_Add = selectedTagIdsList.Join(oldSelectedTagIdsList, p => p, p => p, (first, second) => first).ToList();
				var addedTagsList = selectedTagIdsList.Except(shared_Add).ToList();
				await AddTags(addedTagsList, obj, language);
			}
		}

		private async Task DeletePropertyInventoryTags(List<Guid>? selectedTagIdsList, PropertyInventory obj, List<Guid> oldSelectedTagIdsList)
		{
			if (selectedTagIdsList != null)
			{
				var shared_Delete = oldSelectedTagIdsList.Join(selectedTagIdsList, p => p, p => p, (first, second) => first).ToList();
				var deletedTagsList = oldSelectedTagIdsList.Except(shared_Delete).ToList();
				if (deletedTagsList != null && deletedTagsList.Any())
				{
					foreach (var tagId in deletedTagsList)
					{
						var articleTag = await GetAll<PropertyTag>(entity => entity.PropertyInventoryId == obj.Id && entity.TagId == tagId).FirstOrDefaultAsync();
						if (articleTag != null)
						{
							Delete(articleTag);
						}
					}
				}
			}
		}
		private async Task<UploadFileResultDTO> UpdateDocuments(PropertyInventoryDTO model, PropertyInventory obj)
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
                result.AddedFilesList= model.FilesList;
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

        private async Task updatePropertyInventory(string? title, string? description, string? shortDescription, Language language, Guid propertyInventoryId)
		{
			if (!string.IsNullOrEmpty(title))
			{
				var obj_Title = await GetAll<PropertyInventoryTitle>(entity => entity.PropertyInventoryId == propertyInventoryId && entity.Language == language).FirstOrDefaultAsync();
                if (obj_Title != null)
                {
                    obj_Title.Title = title;
                    obj_Title.UrlTitle = Regex.Replace(title.Trim(), @"\s+", " ").Replace(" ", "_");
                    obj_Title.Description = description;
					obj_Title.ShortDescription = shortDescription;
                    Edit(obj_Title);
                }
                else
                {
                    obj_Title = new PropertyInventoryTitle
                    {
                        Title = title,
                        UrlTitle = Regex.Replace(title.Trim(), @"\s+", " ").Replace(" ", "_"),
                        Description = description,
						ShortDescription = shortDescription,
                        PropertyInventoryId = propertyInventoryId,
                        Language = language
                    };
                    await AddAsync(obj_Title);
                }
			}
		}
		public async Task<List<FileUploadViewModelPost>> DeletePropertyInventoryAsync(Guid id, CancellationToken cancellationToken)
        {
            var obj = await GetAsync<PropertyInventory>(id) ?? throw new NotFoundException("PropertyInventory");
            Delete(obj);
            var documentsList = await getDocumentsList(id);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return documentsList;
        }

        private async Task<List<FileUploadViewModelPost>> getDocumentsList(Guid id)
        {
            return await GetAll<Document>(entity => entity.RelatedId == id && entity.EntityType == EntityType.PropertyInventor)
                .Select(p => new FileUploadViewModelPost
                {
                    FileName = p.FileName,
                    FilePath = p.FilePath,
                    Id = p.Id
                   
                }).ToListAsync();
        }
    }
}
