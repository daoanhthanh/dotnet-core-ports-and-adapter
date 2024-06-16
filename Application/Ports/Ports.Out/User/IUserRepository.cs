using Application.Domain.Core.Interfaces;
using DomainModels = Domain.Core.User;

namespace Ports.Out.User;


public interface IUserRepository: IRepository<DomainModels.User>
{
    Task<DomainModels.User?> FindByPhoneOrEmail(string phoneOrEmail, bool includePassword = false);
}
