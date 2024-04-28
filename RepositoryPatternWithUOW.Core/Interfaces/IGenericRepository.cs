using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPatternWithUOW.Core.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetById(int id);
        Task<T> Add(T entity);
        Task<IEnumerable<T>> GetAll();
        Task<T> Find(Expression<Func<T, bool>> match, string[] includes = null);
    }
}
