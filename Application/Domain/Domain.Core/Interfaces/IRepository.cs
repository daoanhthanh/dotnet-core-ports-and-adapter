namespace Application.Domain.Core.Interfaces;

public interface IRepository<TEntity> : IDisposable
    where TEntity : class
{
    void Add(TEntity? obj);

    TEntity? FindById(Guid id);

    Task<IQueryable<TEntity>> FindAll();

    IQueryable<TEntity> FindAll(ISpecification<TEntity> spec);

    IQueryable<TEntity?> FindAllSoftDeleted();

    void Update(TEntity obj);

    void Remove(TEntity obj);

    int SaveChanges();
}
