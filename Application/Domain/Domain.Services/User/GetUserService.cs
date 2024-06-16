using AutoMapper;
using Ports.In.User;
using Ports.Out.User;
using DomainModel = Domain.Core.User;

namespace Domain.Services.User;

public class GetUserService(
    IMapper mapper,
    IUserRepository repo) : GetUser
{
    public override IMapper Mapper { get; } = mapper;

    protected override Task<DomainModel.User?> _ByPhoneOrEmail(string input)
    {
        return repo.FindByPhoneOrEmail(input);
    }

    protected override Task<IQueryable<DomainModel.User>> _All()
    {
       return repo.FindAll();
    }
}