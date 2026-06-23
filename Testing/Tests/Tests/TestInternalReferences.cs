namespace Testing.Tests;

class InternalReferencesA
{
    // Internals are accessible by default
    Definitions.Internal.Default a = new();
    Definitions.Internal.DefaultClassSpecified b = new();

    // AccessibleByInternalAndExternal and InternalAccessOnly both work when "this" class is specified
    Definitions.Internal.InternalOnlyClassSpecified e = new();
    Definitions.Internal.BothClassSpecified f = new();

    // AccessibleByInternalAndExternal and InternalAccessOnly both work without issue when class unspecified
    Definitions.Internal.InternalOnly g = new();
    Definitions.Internal.Both h = new();

    // Wild card with correct ns should work
    Definitions.Internal.WildCard i = new();

    // CACC000; external only's cannot be referenced
    #region CACC000 ExternalOnly,ExternalOnlyClassSpecified,IncorrectWildCard
    Definitions.Internal.ExternalOnly c = new();
    Definitions.Internal.ExternalOnlyClassSpecified d = new();
    Definitions.Internal.IncorrectWildCard j = new();
    #endregion
}

// Similar to class A, but 'ClassSpecified' attributes do not point to this class
class InternalReferencesB
{
    // CACC000; this class is not allow-listed
    #region CACC000 InternalOnlyClassSpecified,BothClassSpecified,IncorrectWildCard
    Definitions.Internal.InternalOnlyClassSpecified e = new();
    Definitions.Internal.BothClassSpecified f = new();
    #endregion
}
