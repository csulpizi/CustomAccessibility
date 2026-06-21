namespace Testing.Tests;

class MethodDeclarationClass
{
    // Testing return type success
    Definitions.Internal.InternalOnly A() => new();

    // Testing parameter type success
    void B(Definitions.Internal.InternalOnly x) { }

    // CACC000; this class is not allow-listed
#pragma warning disable CACC000 // Restricted Access
    Definitions.Internal.InternalOnlyClassSpecified C() => new();
#pragma warning restore CACC000 // Restricted Access

    // CACC000; this class is not allow-listed
#pragma warning disable CACC000 // Restricted Access
    void D(Definitions.Internal.InternalOnlyClassSpecified x) { }
#pragma warning restore CACC000 // Restricted Access

    // Testing variable declaration within method
    void E()
    {
        // Explicit unrestricted
        Definitions.Internal.InternalOnly a = new();

        // CACC0000; explicit restricted type
#pragma warning disable CACC000 // Restricted Access
        Definitions.Internal.InternalOnlyClassSpecified b = new();
#pragma warning restore CACC000 // Restricted Access

        // Implicit unrestricted
        var c = new Definitions.Internal.InternalOnly();

        // CACC0000; implicit restricted type
#pragma warning disable CACC000 // Restricted Access
        var dc = new Definitions.Internal.InternalOnlyClassSpecified();
#pragma warning restore CACC000 // Restricted Access
    }
}
