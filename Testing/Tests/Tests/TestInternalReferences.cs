namespace Testing.Tests;

class InternalReferencesA
{
    // Internals are accessible by default
    Definitions.Internal.Default a = new();
    Definitions.Internal.DefaultClassSpecified b = new();

    // AccessibleByAll and InternalAccessOnly both work when "this" class is specified
    Definitions.Internal.InternalOnlyClassSpecified e = new();
    Definitions.Internal.BothClassSpecified f = new();

    // AccessibleByAll and InternalAccessOnly both work without issue when class unspecified
    Definitions.Internal.InternalOnly g = new();
    Definitions.Internal.Both h = new();

    // Wild card with correct ns should work
    Definitions.Internal.WildCard i = new();

    // CACC000; external only's cannot be referenced
#pragma warning disable CACC000 // Restricted Access
    Definitions.Internal.ExternalOnly c = new();
#pragma warning restore CACC000 // Restricted Access
#pragma warning disable CACC000 // Restricted Access
    Definitions.Internal.ExternalOnlyClassSpecified d = new();
#pragma warning restore CACC000 // Restricted Access

    // CACC000; wild card with incorrect ns should not work
#pragma warning disable CACC000 // Restricted Access
    Definitions.Internal.IncorrectWildCard j = new();
#pragma warning restore CACC000 // Restricted Access
}

// Similar to class A, but 'ClassSpecified' attributes do not point to this class
class InternalReferencesB
{
    // CACC000; this class is not allow-listed
#pragma warning disable CACC000 // Restricted Access
    Definitions.Internal.InternalOnlyClassSpecified e = new();
#pragma warning restore CACC000 // Restricted Access
#pragma warning disable CACC000 // Restricted Access
    Definitions.Internal.BothClassSpecified f = new();
#pragma warning restore CACC000 // Restricted Access
}
