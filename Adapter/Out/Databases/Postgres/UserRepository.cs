using Adapter.Out.Postgres.Base;
using Microsoft.EntityFrameworkCore;
using Ports.Out.User;
using DomainModel = Domain.Core.User;

namespace Adapter.Out.Postgres;


public class UserRepository(
    PostgresRootDbContext context
) : Repository<DomainModel.User>(context), IUserRepository
{
    public Task<DomainModel.User?> FindByPhoneOrEmail(string phoneOrEmail, bool includePassword)
    {
        if (includePassword)
        {
            return DbSet
                .Include(u => u.Passwords)
                .FirstOrDefaultAsync(u => u.PhoneNumber == phoneOrEmail || u.Email == phoneOrEmail);
        }

        return DbSet.FirstOrDefaultAsync(u => u.PhoneNumber == phoneOrEmail || u.Email == phoneOrEmail);
    }
}