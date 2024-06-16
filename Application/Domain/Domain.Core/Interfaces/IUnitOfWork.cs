namespace Application.Domain.Core.Interfaces;

public interface IUnitOfWork : IDisposable
{
    bool Commit();
}
