using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CandidateManagement.Data.Repositories;

// ------------------------------------------------------------
// RepositoryBase: Triển khai generic repository chung
// ------------------------------------------------------------
public class RepositoryBase<TEntity> : IRepository<TEntity> where TEntity : class
{
    protected readonly AppDbContext Context;
    protected readonly DbSet<TEntity> DbSet;

    public RepositoryBase(AppDbContext context)
    {
        Context = context;
        DbSet = Context.Set<TEntity>();
    }

    public virtual async Task<IEnumerable<TEntity>> GetAllAsync() =>
        await DbSet.AsNoTracking().ToListAsync();

    public virtual async Task<TEntity?> GetByIdAsync(int id) =>
        await DbSet.FindAsync(id);

    public virtual async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate) =>
        await DbSet.AsNoTracking().Where(predicate).ToListAsync();

    public virtual async Task AddAsync(TEntity entity)
    {
        await DbSet.AddAsync(entity);
    }

    public virtual void Update(TEntity entity)
    {
        DbSet.Update(entity);
    }

    public virtual void Remove(TEntity entity)
    {
        DbSet.Remove(entity);
    }

    public Task<int> SaveChangesAsync() => Context.SaveChangesAsync();
}

