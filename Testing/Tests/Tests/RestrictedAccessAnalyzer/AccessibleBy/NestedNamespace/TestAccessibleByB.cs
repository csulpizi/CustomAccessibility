namespace Testing.Tests.RestrictedAccessAnalyzer.AccessibleBy.NestedNamespace;

class Dog
{
    Definitions.Internal.ByDog w = new();
    Definitions.Internal.ByDogAlt x = new();
    Definitions.Internal.ByPrefixDog y = new();

    // Not a direct child of namespace; it's nested
    #region CACC000 ByOuterNamespace,ByFullyQualified
    Definitions.Internal.ByOuterNamespace z = new();
    Definitions.Internal.ByEntireNamespace a = new();
    Definitions.Internal.ByFullyQualified b = new();
    #endregion
    Definitions.Internal.ByCompleteWildCard c = new();
}
