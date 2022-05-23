using AutoMapper;

namespace MassTransit.Shared.Infrastructure.AutoMapperExtensions.Contracts
{
    public interface IMapTo<T>
    {
        void CreateMap(Profile profile) => profile.CreateMap(GetType(), typeof(T));
    }
}