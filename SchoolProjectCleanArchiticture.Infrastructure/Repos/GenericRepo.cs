﻿using Microsoft.EntityFrameworkCore;
using SchoolProjectCleanArchiticture.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProjectCleanArchiticture.Infrastructure.Repos
{
    public class GenericRepo<T> : IGernericRepos<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        public GenericRepo(ApplicationDbContext applicationDbcontext)
        {
            _context = applicationDbcontext;
        }

        public async Task<T> AddAsync(T entity)
        {
           await  _context.Set<T>().AddAsync(entity);
            await  _context.SaveChangesAsync();
            return entity;
        }

        public async  Task AddRangeAsync(ICollection<T> entity)
        {
            foreach (var item in entity)
            {
                _context.Set<T>().Add(item);
                _context.SaveChanges();    
            }
        }

        public void CommitChanges()
        {
           _context.Database.CommitTransaction();
        }

        public  async Task DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
           await  _context.SaveChangesAsync();

        }

        public async  Task DeleteRangeAsync(ICollection<T> entities)
        {
            _context.RemoveRange(entities);
            await _context.SaveChangesAsync();
        }

       

        public async Task<T> GetByIdAsync(int id)
        {
            var result= await _context.Set<T>().FindAsync(id);
            await _context.SaveChangesAsync();
            return result;
        }

        public IQueryable<T> GetTableAsNoTracking()
        {
         
            return _context.Set<T>().AsNoTracking().AsQueryable();
        }

        public IQueryable<T> GetTableAsTracking()
        {
            return _context.Set<T>().AsQueryable();
        }

        public IQueryable<T> GetTableIQueryable()
        {
            return _context.Set<T>().AsQueryable();
        }

        public void RollBack()
        {
           _context.Database.RollbackTransaction();
        }

        public async Task UpdateAsync(T entity)
        {
             _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateRange(ICollection<T> entities)
        {
           _context.Set<T>().UpdateRange(entities);
           await _context.SaveChangesAsync();
        }
    }
}
