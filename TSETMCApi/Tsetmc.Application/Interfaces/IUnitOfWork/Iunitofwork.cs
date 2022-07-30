using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsetmc.Application.Interfaces.IUnitOfWork
{
    public interface Iunitofwork : IDisposable
    {
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        int savechanges();
        Task<int> savechangesAsync(CancellationToken cancellationToken=default);
       int TruncateTable();
       
    }
}
