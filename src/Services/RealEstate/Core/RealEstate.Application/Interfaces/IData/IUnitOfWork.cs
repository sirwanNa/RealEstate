using Microsoft.EntityFrameworkCore;

namespace RealEstate.Application.Interfaces.IData
{
    public interface IUnitOfWork
    {
        DbContext Context { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
