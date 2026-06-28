namespace Testing.Tests;

class InternalReferencesA
{
    // Internals are accessible by default
    Definitions.Internal.Default a = new();
    Definitions.Internal.DefaultClassSpecified b = new();

    // ExternalAccessAllowed and InternalAccessOnly both work without issue when class unspecified
    Definitions.Internal.ExternalAllowed h = new();

    // Wild card with correct ns should work
    Definitions.Internal.WildCard i = new();

    // CACC000; external only's cannot be referenced
    #region CACC000 IncorrectWildCard
    Definitions.Internal.IncorrectWildCard j = new();
    #endregion
}

// Similar to class A, but 'ClassSpecified' attributes do not point to this class
class InternalReferencesB
{
    // CACC000; this class is not allow-listed
    #region CACC000 ExternalAllowedClassSpecified
    Definitions.Internal.ExternalAllowedClassSpecified f = new();
    #endregion
}
