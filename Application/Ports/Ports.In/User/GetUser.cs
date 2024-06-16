using AutoMapper;
using AutoMapper.QueryableExtensions;
using Ports.In.User.DTOs;
using DomainModels = Domain.Core.User;

namespace Ports.In.User;

public abstract class GetUser : IEntityMapper
{
    public abstract IMapper Mapper { get; }

    public async Task<IEnumerable<UserViewModel>> All()
    {
        var result = await _All();
        return result.ProjectTo<UserViewModel>(Mapper.ConfigurationProvider);
    }

    public async Task<UserViewModel?> ByPhoneOrEmail(string input)
    {
        var result = await _ByPhoneOrEmail(input);
        return Mapper.Map<UserViewModel?>(result);
    }


    protected abstract Task<DomainModels.User?> _ByPhoneOrEmail(string input);

    protected abstract Task<IQueryable<DomainModels.User>> _All();
}
