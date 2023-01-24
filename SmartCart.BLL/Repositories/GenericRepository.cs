using Microsoft.EntityFrameworkCore;
using SmartCart.BLL.Interfaces;
using SmartCart.BLL.Repositories.Specifications;
using SmartCart.DAl.Data;
using SmartCart.DAl.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCart.BLL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly StoreContext _storeContext;

        public GenericRepository(StoreContext storeContext)
        {
            _storeContext=storeContext;
        }

       
        public async Task<IReadOnlyList<T>> GetAllAsync()
            =>  await _storeContext.Set<T>().ToListAsync();
      

        public async Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
            => await _storeContext.Set<T>().FindAsync(id);
        

        public async Task<T> GetByIdWithSpecAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync();
        }

        public async Task<int> GetCountAsync(ISpecification<T> spec)
            => await ApplySpecification(spec).CountAsync();


        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(_storeContext.Set<T>(), spec);
        }

        public async Task Add(T entity)
         => await _storeContext.Set<T>().AddAsync(entity);

        public void Delete(T entity)
            => _storeContext.Set<T>().Remove(entity);

        public void Update(T entity)
            => _storeContext.Set<T>().Update(entity);
           //=> _storeContext.Entry(entity).State = EntityState.Modified;
    }
}
