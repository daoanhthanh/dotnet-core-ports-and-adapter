using Adapter.Database.Postgres.Base;
using Application.Domain.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Adapter.Out.Postgres.Base;


public abstract class Repository<TEntity> : IRepository<TEntity>
    where TEntity : class
{
    protected readonly DbContext Db;
    protected readonly DbSet<TEntity> DbSet;

    public Repository(DbContext context)
    {
        Db = context;
        DbSet = Db.Set<TEntity>();
    }

    public virtual void Add(TEntity? obj)
    {
        DbSet.Add(obj);
    }

    public virtual TEntity? FindById(Guid id)
    {
        return DbSet.Find(id);
    }

    public virtual Task<IQueryable<TEntity>> FindAll()
    {
        DbSet.AsNoTracking();
        return Task.FromResult(DbSet.AsQueryable());
    }

    public virtual IQueryable<TEntity> FindAll(ISpecification<TEntity> spec)
    {
        return ApplySpecification(spec);
    }

    public virtual IQueryable<TEntity?> FindAllSoftDeleted()
    {
        return DbSet.IgnoreQueryFilters()
            .Where(e => EF.Property<bool>(e, "IsDeleted") == true);
    }

    public virtual void Update(TEntity obj)
    {
        DbSet.Update(obj);
    }

    public virtual void Remove(TEntity obj)
    {
        DbSet.Remove(obj);
    }

    public int SaveChanges()
    {
        return Db.SaveChanges();
    }

    public void Dispose()
    {
        Db.Dispose();
        GC.SuppressFinalize(this);
    }

    private IQueryable<TEntity> ApplySpecification(ISpecification<TEntity> spec)
    {
        return SpecificationEvaluator<TEntity>.GetQuery(DbSet.AsQueryable(), spec);
    }
}
