namespace Testing.Tests.RestrictedAccessAnalyzer.AccessibleBy;

class Dog
{
    Definitions.Internal.ByDog w = new();
    Definitions.Internal.ByDogAlt x = new();
    Definitions.Internal.ByOuterNamespace z = new();
    Definitions.Internal.ByEntireNamespace a = new();
    #region CACC000 ByFullyQualified
    Definitions.Internal.ByFullyQualified b = new();
    #endregion
    Definitions.Internal.ByCompleteWildCard c = new();
}

class OuterDog
{
    #region CACC000 ByDog,ByDogAlt
    Definitions.Internal.ByDog w = new();
    Definitions.Internal.ByDogAlt x = new();
    #endregion
    Definitions.Internal.ByPrefixDog y = new();
    Definitions.Internal.ByOuterNamespace z = new();
    Definitions.Internal.ByEntireNamespace a = new();
    #region CACC000 ByFullyQualified
    Definitions.Internal.ByFullyQualified b = new();
    #endregion
    Definitions.Internal.ByCompleteWildCard c = new();

    class Dog
    {
        Definitions.Internal.ByDog w = new();
        Definitions.Internal.ByDogAlt x = new();
        Definitions.Internal.ByPrefixDog y = new();

        // Not a direct child of namespace; it's nested
        #region CACC000 ByOuterNamespace
        Definitions.Internal.ByOuterNamespace z = new();
        #endregion
        Definitions.Internal.ByEntireNamespace a = new();
        Definitions.Internal.ByFullyQualified b = new();
        Definitions.Internal.ByCompleteWildCard c = new();
    }
}
