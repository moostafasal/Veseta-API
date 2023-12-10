using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Veseta.Core.entites;
using Veseta.Core.Specifications;


namespace Veseta.Core.IRepository
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<IReadOnlyList<T>> GetAllAsync();
        //Task<T> GetByIdAsync(int id);
        Task<IList<T>> GetAllWithSpecAsync(ISpecification<T> spec);
        Task<T> GetByIdWithSpecAsync(ISpecification<T> spec);
        //Task<T> GetByIdWithSpecAsync(ISpecification<T> spec);
        Task<int> GetCount();
        Task<IList<T>> GetPatchById(IList<int> patchIds);
        Task<T> GetByIdAsync(int id);
        Task<T> GetUserByIdAsync(string id);
        Task Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        void DeletePatch(IList<T> entities); 
        
    }
}
