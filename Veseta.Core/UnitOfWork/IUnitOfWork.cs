
using Veseta.Core.entites;
using Veseta.Core.IRepository;



namespace Veseta.Core.UnitOfWork
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IGenericRepository<TEntity>? Repository<TEntity>() where TEntity : BaseEntity;

        Task<int> Complete();
    }
}
