using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CandidateManagement.Data.Repositories;

// ------------------------------------------------------------
// IRepository: Interface generic cho các repository
// ------------------------------------------------------------
public interface IRepository<TEntity> where TEntity : class
{
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<TEntity?> GetByIdAsync(int id);
    Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);
    Task AddAsync(TEntity entity);
    void Update(TEntity entity);
    void Remove(TEntity entity);
    Task<int> SaveChangesAsync();
}

