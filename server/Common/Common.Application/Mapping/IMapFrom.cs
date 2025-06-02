namespace WodItEasy.Common.Application.Mapping
{
    using AutoMapper;

    public interface IMapFrom<TSource>
    {
        void Mapping(Profile mapper) 
            => mapper.CreateMap(typeof(TSource), this.GetType());
    }
}
