using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Contracts.Interfaces;
using Domain.Entities;
using Persistence.Data;

namespace Persistence.Repositories.Common
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public UnitOfWork(ApplicationDbContext context) => _context = context;
       

        public async Task<int> CompleteAsync() => await _context.SaveChangesAsync();
        

        public void Dispose() => _context.Dispose();
       

        public IGenericRepository<T> GenericRepository<T>() where T : class => new GenericRepository<T>(_context);

    }
}
