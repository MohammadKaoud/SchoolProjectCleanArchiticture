using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProjectCleanArchiticture.Infrastructure.Repos
{
    public  interface IGernericRepos <T> where T : class
    {
        public IQueryable<T> GetTableIQueryable();
         public Task<T> GetByIdAsync(int id);
         public Task DeleteRangeAsync(ICollection<T> entities);
         public Task DeleteAsync(T entity);
        public Task<T>AddAsync (T entity);
        public Task AddRangeAsync(ICollection<T> entity);
       
        public void RollBack();
        public void CommitChanges();
        //
        public Task UpdateAsync(T entity);
        public Task UpdateRange(ICollection<T> entities);

  
        public IQueryable<T> GetTableAsNoTracking();
        public IQueryable<T> GetTableAsTracking();
        


    }
}
