namespace AwesomeBank.BuildingBlocks.Infrastructure
{
    using System.Collections.Generic;
    using AwesomeBank.BuildingBlocks.Application;

    public class Mapper : IMapper
    {
        private readonly AutoMapper.IMapper _mapper;

        public Mapper(AutoMapper.IMapper mapper)
        {
            _mapper = mapper;
        }

        public TDestination Map<TSource, TDestination>(TSource source)
            => _mapper.Map<TSource, TDestination>(source);

        public IEnumerable<TDestination> MapCollection<TSource, TDestination>(IEnumerable<TSource> sources)
            => _mapper.Map<IEnumerable<TSource>, IEnumerable<TDestination>>(sources);
    }
}