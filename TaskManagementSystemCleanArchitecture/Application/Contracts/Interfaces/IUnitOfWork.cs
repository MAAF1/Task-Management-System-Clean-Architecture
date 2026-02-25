using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
namespace Application.Contracts.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<TaskEntity> Tasks {  get; }

        Task<int> CompleteAsync();
    }
}
