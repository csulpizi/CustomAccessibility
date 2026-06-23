namespace Testing.Tests;

class MethodDeclarationClass
{
    // Testing return type success
    Definitions.Internal.InternalOnly A() => new();

    // Testing parameter type success
    void B(Definitions.Internal.InternalOnly x) { }

    // CACC000; this class is not allow-listed
    #region CACC000 InternalOnlyClassSpecified
    Definitions.Internal.InternalOnlyClassSpecified C() => new();

    void D(Definitions.Internal.InternalOnlyClassSpecified x) { }
    #endregion

    // Testing variable declaration within method
    void E()
    {
        // Explicit unrestricted
        Definitions.Internal.InternalOnly a = new();

        // CACC0000; explicit restricted type
        #region CACC000 InternalOnlyClassSpecified
        Definitions.Internal.InternalOnlyClassSpecified b = new();
        #endregion

        // Implicit unrestricted
        var c = new Definitions.Internal.InternalOnly();

        // CACC0000; implicit restricted type
        #region CACC000 InternalOnlyClassSpecified,var
        var dc = new Definitions.Internal.InternalOnlyClassSpecified();
        #endregion
    }
}
