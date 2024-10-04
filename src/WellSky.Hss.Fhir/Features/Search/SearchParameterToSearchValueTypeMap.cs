namespace WellSky.Hss.Fhir.Features.Search
{
    using System.Collections.Concurrent;
    using System.Diagnostics;
    using Microsoft.Health.Fhir.Core.Features.Search;
    using Microsoft.Health.Fhir.Core.Features.Search.SearchValues;
    using Microsoft.Health.Fhir.Core.Models;
    using SearchParamType = Microsoft.Health.Fhir.ValueSets.SearchParamType;

    internal sealed class SearchParameterToSearchValueTypeMap
    {
        private readonly ConcurrentDictionary<SearchParameterInfo, Type> _compositeMap = new ConcurrentDictionary<SearchParameterInfo, Type>();

        public Type GetSearchValueType(SearchParameterInfo searchParameter)
        {
            if (searchParameter.Type != SearchParamType.Composite)
            {
                return GetSearchValueTypeForSearchParameterType(searchParameter.Type);
            }

            if (!_compositeMap.TryGetValue(searchParameter, out Type? type))
            {
                type = GetCompositeSearchValueType(searchParameter);

                _compositeMap.TryAdd(searchParameter, type);
            }

            return type;
        }

        public Type GetSearchValueType(SearchIndexEntry searchIndexEntry)
        {
            if (searchIndexEntry.Value is CompositeSearchValue)
            {
                return GetSearchValueType(searchIndexEntry.SearchParameter);
            }

            Type searchValueType = searchIndexEntry.Value.GetType();

            Debug.Assert(searchValueType == GetSearchValueType(searchIndexEntry.SearchParameter), "Getting the search value type from the search parameter produced a different result from calling searchValue.GetType()");

            return searchValueType;
        }

        private static Type GetCompositeSearchValueType(SearchParameterInfo searchParameter)
        {
            Debug.Assert(searchParameter.Type == SearchParamType.Composite, "Method called with non-composite search parameter");

            return typeof(Tuple).Assembly.GetType($"{typeof(ValueTuple).FullName}`{searchParameter.Component.Count}", throwOnError: true)
                .MakeGenericType(searchParameter.Component.Select(component => GetSearchValueTypeForSearchParameterType(component.ResolvedSearchParameter.Type)).ToArray());
        }

        private static Type GetSearchValueTypeForSearchParameterType(SearchParamType searchParameterType) =>
            searchParameterType switch
            {
                SearchParamType.Number => typeof(NumberSearchValue),
                SearchParamType.Date => typeof(DateTimeSearchValue),
                SearchParamType.String => typeof(StringSearchValue),
                SearchParamType.Token => typeof(TokenSearchValue),
                SearchParamType.Reference => typeof(ReferenceSearchValue),
                SearchParamType.Quantity => typeof(QuantitySearchValue),
                SearchParamType.Uri => typeof(UriSearchValue),
                _ => throw new ArgumentOutOfRangeException(nameof(searchParameterType)),
            };
    }
}
