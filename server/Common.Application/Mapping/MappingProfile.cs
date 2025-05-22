namespace WodItEasy.Common.Application.Mapping
{
    using System.Reflection;
    using AutoMapper;

    public class MappingProfile : Profile
    {
        public MappingProfile() 
            => this.ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());

        private void ApplyMappingsFromAssembly(Assembly assembly)
            => assembly
                .GetExportedTypes()
                .Where(type => type
                    .GetInterfaces()
                    .Any(interf =>
                        interf.IsGenericType &&
                        interf.GetGenericTypeDefinition() == typeof(IMapFrom<>)))
                .ToList()
                .ForEach(type =>
                {
                    var instance = Activator.CreateInstance(type);
                    const string mappingMethodName = "Mapping";
                    var methodInfo = type
                        .GetMethod(mappingMethodName)
                        ?? type
                            .GetInterface("IMapFrom`1")
                            ?.GetMethod(mappingMethodName);

                    methodInfo?.Invoke(instance, new object[] { this });
                });
    }
}
