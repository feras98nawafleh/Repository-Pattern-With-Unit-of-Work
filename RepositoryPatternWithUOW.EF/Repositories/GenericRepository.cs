using Microsoft.EntityFrameworkCore;
using RepositoryPatternWithUOW.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPatternWithUOW.EF.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected ApplicationDbContext _context { get; set; }
        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<T> GetById(int id)
        {
            var response = await _context.Set<T>().FindAsync(id);
            return response;
        }

        public async Task<T> Add(T entity)
        {
            _context.Set<T>().Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _context.Set<T>().Include("Author").ToListAsync();
        }

        public async Task<T> Find(Expression<Func<T, bool>> match, string[] includes = null)
        {
            IQueryable<T> query = _context.Set<T>();
            if(includes != null)
                foreach(var include in includes)
                    query = query.Include(include);
              
            return await query.SingleOrDefaultAsync(match);
        }
    }
}
