using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Application.Contracts.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Persistence.Repositories.Common
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        internal DbSet<T> _dbSet;
        public GenericRepository(ApplicationDbContext context)
        {
            
            _context = context;
            _dbSet = _context.Set<T>();
        }
        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void Delete(T entity) => _dbSet.Remove(entity);
        

        public async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.ToListAsync();
        
      
        public async Task<T> GetByIdAsync(int id) => await _dbSet.FindAsync(id);

       

        public void Update(T entity) => _dbSet.Update(entity);
        
    }
}
