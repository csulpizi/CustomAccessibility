namespace Testing.Tests;

class MethodDeclarationClass
{
    // Testing return type success
    Definitions.Internal.Default A() => new();

    // Testing parameter type success
    void B(Definitions.Internal.Default x) { }

    // CACC000; this class is not allow-listed
    #region CACC000 DefaultClassSpecified
    Definitions.Internal.DefaultClassSpecified C() => new();

    void D(Definitions.Internal.DefaultClassSpecified x) { }
    #endregion

    // Testing variable declaration within method
    void E()
    {
        // Explicit unrestricted
        Definitions.Internal.Default a = new();

        // CACC0000; explicit restricted type
        #region CACC000 DefaultClassSpecified
        Definitions.Internal.DefaultClassSpecified b = new();
        #endregion

        // Implicit unrestricted
        var c = new Definitions.Internal.Default();

        // CACC0000; implicit restricted type
        #region CACC000 DefaultClassSpecified,var
        var dc = new Definitions.Internal.DefaultClassSpecified();
        #endregion
    }
}
