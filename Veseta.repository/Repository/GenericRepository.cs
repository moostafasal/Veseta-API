using Microsoft.EntityFrameworkCore;

using Veseta.Core.entites;
using Veseta.Core.IRepository;
using Veseta.Core.Specifications;
using Veseta.repository.Data;


namespace Veseta.CoreRepository.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly ApplicationDbContext _context;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Add(T entity)
            => await _context.Set<T>().AddAsync(entity);

        public void Delete(T entity)
            => _context.Set<T>().Remove(entity);
        public void DeletePatch(IList<T> entities)
            => _context.Set<T>().RemoveRange(entities);

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<IList<T>> GetAllWithSpecAsync(ISpecification<T> spec)
        {
            return await ApplySpecifications(spec).ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<T> GetByIdWithSpecAsync(ISpecification<T> spec)
        {
            return await ApplySpecifications(spec).SingleOrDefaultAsync();
        }

        public async Task<int> GetCount()
        {
            return await _context.Set<T>().CountAsync();
        }

        public async Task<IList<T>> GetPatchById(IList<int> patchIds)
        {
            return await _context.Set<T>().Where(t => patchIds.Contains(t.Id)).ToListAsync();
        }

        public async Task<T> GetUserByIdAsync(string id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public void Update(T entity)
            => _context.Set<T>().Update(entity);

        private IQueryable<T> ApplySpecifications(ISpecification<T> spec)
        {
            return SpecificationEvalutor<T>.GetQuery(_context.Set<T>(), spec);
        }
    }
}
