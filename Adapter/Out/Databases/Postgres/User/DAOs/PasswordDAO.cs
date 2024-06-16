using Adapter.Out.Postgres.Base;
using Domain.Core.User;

namespace Adapter.Out.Postgres.User.DAOs;

public class PasswordDao(PostgresRootDbContext context) : Repository<UserPassword>(context)
{
}