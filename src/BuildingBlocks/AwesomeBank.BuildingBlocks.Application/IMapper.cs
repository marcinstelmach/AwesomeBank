namespace AwesomeBank.BuildingBlocks.Application
{
    using System.Collections.Generic;

    public interface IMapper
    {
        TDestination Map<TSource, TDestination>(TSource source);

        IEnumerable<TDestination> MapCollection<TSource, TDestination>(IEnumerable<TSource> sources);
    }
}