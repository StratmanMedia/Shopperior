using Microsoft.EntityFrameworkCore;

namespace StratmanMedia.Repositories.EFCore;

public class Repository<TContext, TEntity> : IRepository<TEntity>
    where TContext : DbContext
    where TEntity : class
{
    protected TContext Context { get; }
    protected DbSet<TEntity> Table => Context.Set<TEntity>();

    public Repository(TContext context)
    {
        Context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public virtual void Create(TEntity entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));
        Table.Attach(entity);
        Table.Add(entity);
        Context.Entry(entity).State = EntityState.Added;
        var result = Context.SaveChanges();
        if (result == 0) throw new InvalidOperationException("No new rows were created in the database.");
    }

    public virtual async Task CreateAsync(TEntity entity, CancellationToken ct = new())
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));
        Table.Attach(entity);
        await Table.AddAsync(entity, ct);
        Context.Entry(entity).State = EntityState.Added;
        var result = await Context.SaveChangesAsync(ct);
        if (result == 0) throw new InvalidOperationException("No new rows were created in the database.");
    }

    public virtual void Delete(TEntity entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));
        Table.Attach(entity);
        Table.Remove(entity);
        Context.SaveChanges();
    }

    public virtual async Task DeleteAsync(TEntity entity, CancellationToken ct = new())
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));
        Table.Attach(entity);
        Table.Remove(entity);
        await Context.SaveChangesAsync(ct);
    }

    public virtual IEnumerable<TEntity> GetAll()
    {
        return Table.ToArray();
    }

    public virtual async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken ct = new())
    {
        return await Task.Run(GetAll, ct);
    }
}