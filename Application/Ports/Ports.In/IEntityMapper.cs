using AutoMapper;

namespace Ports.In;

public interface IEntityMapper
{
    public IMapper Mapper { get; }
}