using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IDataBaseContext
    {
        DbSet<T> Set<T>() where T : class;
        public DbSet<Subsidiary> Subsidiaries { get; set; }
        public DbSet<Master> Masters { get; set; }
       

        int SaveChanges();  
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
