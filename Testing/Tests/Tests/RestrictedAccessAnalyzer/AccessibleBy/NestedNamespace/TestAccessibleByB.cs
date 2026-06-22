namespace Testing.Tests.RestrictedAccessAnalyzer.AccessibleBy.NestedNamespace;

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
#pragma warning disable CACC000 // Restricted Access
    Definitions.Internal.ByFullyQualified b = new();
#pragma warning restore CACC000 // Restricted Access
Definitions.Internal.ByCompleteWildCard c = new();
}
