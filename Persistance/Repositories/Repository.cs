using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly IDataBaseContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(IDataBaseContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public T GetById(int id) => _dbSet.Find(id);
        public IQueryable<T> GetAll() => _dbSet;
        public void Add(T entity) => _dbSet.Add(entity);
        public void Update(T entity) => _dbSet.Update(entity);
        public void Remove(T entity) => _dbSet.Remove(entity);
        public void Save() => _context.SaveChanges();
    }

}
