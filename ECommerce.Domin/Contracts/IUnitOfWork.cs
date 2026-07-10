using ECommerce.Domin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domin.Contracts
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync(CancellationToken ct = default);
        IGenericRepository<TEntity , TKey> GetRepository<TEntity , TKey>() where TEntity : BaseEntity<TKey>;
    }
}
