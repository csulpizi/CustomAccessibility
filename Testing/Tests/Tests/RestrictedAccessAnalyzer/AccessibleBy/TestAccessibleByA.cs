namespace Testing.Tests.RestrictedAccessAnalyzer.AccessibleBy;

class Dog
{
    Definitions.Internal.ByDog w = new();
    Definitions.Internal.ByDogAlt x = new();
    Definitions.Internal.ByOuterNamespace z = new();
    Definitions.Internal.ByWholeNamespace a = new();
#pragma warning disable CACC000 // Restricted Access
    Definitions.Internal.ByFullyQualified b = new();
#pragma warning restore CACC000 // Restricted Access
    Definitions.Internal.ByCompleteWildCard c = new();
}

class OuterDog
{
#pragma warning disable CACC000 // Restricted Access
    Definitions.Internal.ByDog w = new();
#pragma warning restore CACC000 // Restricted Access
#pragma warning disable CACC000 // Restricted Access
    Definitions.Internal.ByDogAlt x = new();
#pragma warning restore CACC000 // Restricted Access
    Definitions.Internal.ByPrefixDog y = new();
    Definitions.Internal.ByOuterNamespace z = new();
    Definitions.Internal.ByWholeNamespace a = new();
#pragma warning disable CACC000 // Restricted Access
    Definitions.Internal.ByFullyQualified b = new();
#pragma warning restore CACC000 // Restricted Access
Definitions.Internal.ByCompleteWildCard c = new();

    class Dog
    {
            Definitions.Internal.ByDog w = new();
        Definitions.Internal.ByDogAlt x = new();
        Definitions.Internal.ByPrefixDog y = new();
        // Not a direct child of namespace; it's nested
#pragma warning disable CACC000 // Restricted Access
        Definitions.Internal.ByOuterNamespace z = new();
#pragma warning restore CACC000 // Restricted Access
        Definitions.Internal.ByWholeNamespace a = new();
        Definitions.Internal.ByFullyQualified b = new();
        Definitions.Internal.ByCompleteWildCard c = new();
    }
}
