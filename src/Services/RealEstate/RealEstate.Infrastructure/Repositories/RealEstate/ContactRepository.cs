using RealEstate.Application.DTOs.RealEstate;
using RealEstate.Application.DTOs.RealEstate.Queries;
using RealEstate.Application.Exceptions;
using RealEstate.Application.Interfaces.IData;
using RealEstate.Application.Interfaces.IRepository.RealEstate;
using RealEstate.Domain.Entities.RealEstate;
using RealEstate.Infrastructure.Repositories.Common;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace RealEstate.Infrastructure.Repositories.RealEstate
{
	public class ContactRepository(IUnitOfWork unitOfWork, IMapper mapper) : BaseRepository(unitOfWork, mapper), IContactRepository
	{
		public async Task<ContactDTO> GetContactAsync(Guid id)
		{
			var obj = await GetAll<Contact>(entity => entity.Id == id).FirstOrDefaultAsync() ?? throw new NotFoundException("Contact");
			var model = new ContactDTO
			{ 
				CreatedDate =obj.CreatedDate,
				Email=obj.Email,
				Phone=obj.Phone,
				Message=obj.Message,
				Name=obj.Name,
			};
			return model;
		}
		public async Task<ContactListDTO> GetContactsList(ContactQueryDTO queryModel)
		{
			var query = GetAll<Contact>().Where(p=>!p.IsDeleted);
			if (!string.IsNullOrEmpty(queryModel.Name))
			{
				query = query.Where(p=>p.Name.Trim().Contains(queryModel.Name.Trim(), StringComparison.CurrentCultureIgnoreCase));
			}
			if (!string.IsNullOrEmpty(queryModel.Email))
			{
				query = query.Where(p => !string.IsNullOrEmpty(p.Email) && p.Email.Trim().Contains(queryModel.Email.Trim(), StringComparison.CurrentCultureIgnoreCase));
			}
			if (!string.IsNullOrEmpty(queryModel.Phone))
			{
				query = query.Where(p => p.Phone.Trim().Contains(queryModel.Phone.Trim(), StringComparison.CurrentCultureIgnoreCase));
			}
			var count = await query.CountAsync();
			var queryList = await query
			             	 .OrderByDescending(p=>p.CreatedDate)
							 .Skip(queryModel.PageSize * (queryModel.PageNumber - 1))
							.Take(queryModel.PageSize).ToListAsync();
			var model = new ContactListDTO
			{
				PagesCount = count / queryModel.PageSize + (count % queryModel.PageSize > 0 ? 1 : 0),
				ItemsList = queryList.Select(p => new ContactListItemDTO
				{
					Id = p.Id,
					Name=p.Name,
					Email = p.Email,
					Phone = p.Phone,
					Message = p.Message,
					CreatedDate = p.CreatedDate

				}).ToList()
			};

			return model;
		}
		public async Task<int> CreateContactAsync(ContactDTO model, CancellationToken cancellationToken)
		{
			var obj = new Contact 
			{
				Message = model.Message,
				CreatedDate = DateTime.Now,
				Name = model.Name,
				Phone= model.Phone,
				Email = model.Email,
			};
			await AddAsync(obj);
			var code = await _unitOfWork.SaveChangesAsync(cancellationToken);
			return code;
		}
		public async Task<int> DeleteContactAsync(Guid id, CancellationToken cancellationToken)
		{
			var obj = await GetAsync<Contact>(id) ?? throw new NotFoundException("Contact");
			obj.IsDeleted = true;
			var code = await _unitOfWork.SaveChangesAsync(cancellationToken);
			return code;
		}
	}
}
