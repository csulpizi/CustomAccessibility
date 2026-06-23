namespace Testing.Tests;

class ExternalReferencesA
{
    // AccessibleByInternalAndExternal and ExternalAccessOnly both work when "this" class is specified
    readonly Definitions.External.ExternalOnlyClassSpecified e = new();
    readonly Definitions.External.BothClassSpecified f = new();

    // AccessibleByInternalAndExternal and ExternalAccessOnly both work without issue when class unspecified
    readonly Definitions.External.ExternalOnly g = new();
    readonly Definitions.External.Both h = new();

    // Wild card with correct ns should work
    readonly Definitions.External.WildCard i = new();

    // CACC000; wild card with incorrect ns should not work
    #region CACC000 IncorrectWildCard
    readonly Definitions.External.IncorrectWildCard j = new();
    #endregion

    // CACC001; external blocked by default
    #region CACC001 Default,DefaultClassSpecified
    readonly Definitions.External.Default a = new();
    readonly Definitions.External.DefaultClassSpecified b = new();
    #endregion

    // CACC000; internal only's cannot be referenced
    #region CACC000 InternalOnly,InternalOnlyClassSpecified
    readonly Definitions.External.InternalOnly c = new();
    readonly Definitions.External.InternalOnlyClassSpecified d = new();
    #endregion
}

// Similar to class A, but 'ClassSpecified' attributes do not point to this class
class ExternalReferencesB
{
    // CACC000; this class is not allow-listed
    #region CACC000 ExternalOnlyClassSpecified,BothClassSpecified
    readonly Definitions.External.ExternalOnlyClassSpecified e = new();
    readonly Definitions.External.BothClassSpecified f = new();
    #endregion
}

class TestMemberAccess
{
    void Foo(Definitions.External.ClassWithMembers o)
    {
        o.UnrestrictedFoo();
        _ = o.UnrestrictedField;
        _ = o.UnrestrictedProperty;

        #region CACC001 RestrictedFoo,RestrictedField,RestrictedProperty
        o.RestrictedFoo();
        _ = o.RestrictedField;
        _ = o.RestrictedProperty;
        #endregion
    }
}

class TestProtectedAccess : Definitions.External.ClassForProtectedAccessTests
{
    void Foo()
    {
        // None of these fail, all are acceptable
        _ = A;
        _ = B;
        _ = C;
    }
}

class TestProtectedAccessOneMoreLevel : TestProtectedAccess
{
    void Foo()
    {
        // None of these fail, all are acceptable
        _ = A;
        _ = B;
        _ = C;
    }
}