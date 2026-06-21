namespace Testing.Tests;

class ExternalReferencesA
{
    // AccessibleByAll and ExternalAccessOnly both work when "this" class is specified
    readonly Definitions.External.ExternalOnlyClassSpecified e = new();
    readonly Definitions.External.BothClassSpecified f = new();

    // AccessibleByAll and ExternalAccessOnly both work without issue when class unspecified
    readonly Definitions.External.ExternalOnly g = new();
    readonly Definitions.External.Both h = new();

    // Wild card with correct ns should work
    readonly Definitions.External.WildCard i = new();

    // CACC000; wild card with incorrect ns should not work
#pragma warning disable CACC000 // Restricted Access
    readonly Definitions.External.IncorrectWildCard j = new();
#pragma warning restore CACC000 // Restricted Access

    // CACC001; external blocked by default
#pragma warning disable CACC001 // Restricted Access
    readonly Definitions.External.Default a = new();
#pragma warning restore CACC001 // Restricted Access
#pragma warning disable CACC001 // Restricted Access
    readonly Definitions.External.DefaultClassSpecified b = new();
#pragma warning restore CACC001 // Restricted Access

    // CACC000; internal only's cannot be referenced
#pragma warning disable CACC000 // Restricted Access
    readonly Definitions.External.InternalOnly c = new();
#pragma warning restore CACC000 // Restricted Access
#pragma warning disable CACC000 // Restricted Access
    readonly Definitions.External.InternalOnlyClassSpecified d = new();
#pragma warning restore CACC000 // Restricted Access
}

// Similar to class A, but 'ClassSpecified' attributes do not point to this class
class ExternalReferencesB
{
    // CACC000; this class is not allow-listed
#pragma warning disable CACC000 // Restricted Access
    readonly Definitions.External.ExternalOnlyClassSpecified e = new();
#pragma warning restore CACC000 // Restricted Access
#pragma warning disable CACC000 // Restricted Access
    readonly Definitions.External.BothClassSpecified f = new();
#pragma warning restore CACC000 // Restricted Access
}
