using System.Linq.Expressions;
using RealEstate.Application.Interfaces.IData;
using RealEstate.Domain.Common;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace RealEstate.Infrastructure.Repositories.Common
{
    public class BaseRepository(IUnitOfWork unitOfWork,IMapper mapper) 
    {
        protected readonly IUnitOfWork _unitOfWork = unitOfWork;
        protected readonly IMapper _mapper = mapper;
        public async Task<TEntity?> GetAsync<TEntity>(Guid id) where TEntity : BaseEntity
        {
            var result = await _unitOfWork.Context.Set<TEntity>().Where(p => p.Id == id).FirstOrDefaultAsync();
            return result;
        }
        public IQueryable<TEntity> GetAll<TEntity>(Expression<Func<TEntity, bool>>? filter = null, bool tracked = true) where TEntity : BaseEntity
        {
            var query = _unitOfWork.Context.Set<TEntity>().AsQueryable();
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (tracked)
            {
                query = query.AsNoTracking(); 
            }
            return query;
        }
        public void Add<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            _unitOfWork.Context.Set<TEntity>().Add(entity);
        }
        public async Task AddAsync<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            await _unitOfWork.Context.Set<TEntity>().AddAsync(entity);
        }
        public void Delete<TEntity>(Expression<Func<TEntity, bool>> where) where TEntity : BaseEntity
        {
            var items = _unitOfWork.Context.Set<TEntity>().Where(where).AsEnumerable();
            _unitOfWork.Context.Set<TEntity>().RemoveRange(items);
        }

        public void Delete<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            _unitOfWork.Context.Set<TEntity>().Remove(entity);
        }

        public void Edit<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            _unitOfWork.Context.Entry(entity).State = EntityState.Modified;
        }

    }
}
