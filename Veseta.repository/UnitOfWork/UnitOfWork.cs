using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Veseta.Core.entites;
using Veseta.Core.IRepository;
using Veseta.Core.UnitOfWork;
using Veseta.CoreRepository.Repository;
using Veseta.repository.Data;

namespace Veseta.CoreRepository.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;
        private Hashtable _repositories;

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> Complete()
            => await _dbContext.SaveChangesAsync();

        public async ValueTask DisposeAsync()
            => await _dbContext.DisposeAsync();

        public IGenericRepository<TEntity>? Repository<TEntity>() where TEntity : BaseEntity
        {
            if (_repositories is null)
                _repositories = new Hashtable();

            var type = typeof(TEntity).Name;

            if (!_repositories.Contains(type))
            {
                var repository = new GenericRepository<TEntity>(_dbContext);

                _repositories.Add(type, repository);
            }
            return _repositories[type] as GenericRepository<TEntity>;
        }
    }
}
