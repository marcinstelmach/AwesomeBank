namespace AwesomeBank.Tests.Common
{
    using AwesomeBank.BuildingBlocks.Domain.Specifications;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    public static class SpecificationExtensions
    {
        public static IEnumerable<Specification<T>> GetNestedSpecifications<T>(this Specification<T> specification)
        {
            if (specification is AndSpecification<T>)
            {
                var fieldInfos = typeof(AndSpecification<T>)
                    .GetFields(BindingFlags.Instance | BindingFlags.NonPublic);
                return GetSpecificationTypes(fieldInfos, specification);
            }
            else if (specification is OrSpecification<T>)
            {
                var fieldInfos = typeof(OrSpecification<T>)
                    .GetFields(BindingFlags.Instance | BindingFlags.NonPublic);
                return GetSpecificationTypes(fieldInfos, specification);
            }

            return new[] { specification };
        }

        public static bool ContainsSpecification<T, TSpec>(this Specification<T> specification)
            where TSpec : Specification<T>
        {
            var nestedSpecifications = GetNestedSpecifications(specification);
            return nestedSpecifications.OfType<TSpec>().Any();
        }

        private static List<Specification<T>> GetSpecificationTypes<T>(FieldInfo[] fieldInfos, Specification<T> specification)
        {
            var specificationTypes = new List<Specification<T>>();
            specificationTypes.AddRange(
                GetNestedSpecifications(fieldInfos[0].GetValue(specification) as Specification<T>));
            specificationTypes.AddRange(
                GetNestedSpecifications(fieldInfos[1].GetValue(specification) as Specification<T>));
            return specificationTypes;
        }
    }
}